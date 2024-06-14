using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WordNET_Server_2._0.DBRelations;
using WordNET_Server_2._0.DTOs;
using WordNET_Server_2._0.Models;
using WordNET_Server_2._0.Services.ExecutionService;

namespace WordNET_Server_2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController(DBContext dBContext, IExecutionService executionService) : ControllerBase
    {
        private readonly DBContext _dbContext = dBContext;
        private readonly IExecutionService _executionService = executionService;


        [HttpPost("AddAssociatedWord")]
        public async Task<IActionResult> AddAssociatedWord([FromBody] UserAssociatedWordListDTO userAssociatedWordListDTO)
        {
            try
            {
                #region Initial Check
                List<int> wordIds = userAssociatedWordListDTO.KeyWordValueAssociatedWord.Keys.ToList();

                int correctWordsCount = await _dbContext.Word.CountAsync(w => wordIds.Contains(w.Id));

                if (wordIds.Count != correctWordsCount)
                    return BadRequest("One of words ids from DTO doesn't exist");
                #endregion

                #region Adding Associated Word
                using var _tempdbcontext = new DBContext();

                var executionStrategy = _executionService.BeginExecutionStrategy(_tempdbcontext);
                await executionStrategy.ExecuteAsync(async () =>
                {
                    using var transaction = _executionService.BeginTransaction(_tempdbcontext);

                    try
                    {
                        Questionee newQuestionee = new()
                        {
                            IsMan = userAssociatedWordListDTO.IsMan,
                            Age = userAssociatedWordListDTO.Age,
                        };

                        foreach (KeyValuePair<int, string> pairs in userAssociatedWordListDTO.KeyWordValueAssociatedWord)
                        {
                            Word? word = await _tempdbcontext.Word.FindAsync(pairs.Key)
                                ?? throw new Exception($"Can't find word under Id => {pairs.Key}");

                            AssociatedWord? associatedWord = await _tempdbcontext.AssociatedWord
                                .FirstOrDefaultAsync(aw => aw.Name == pairs.Value && aw.WordId == pairs.Key);

                            if (associatedWord is null)
                            {
                                AssociatedWord newAssociatedWord = new()
                                {
                                    Name = pairs.Value,
                                    Count = 1,
                                    WordId = pairs.Key,
                                    Word = word,
                                };

                                // Attach the Questionee only once
                                _tempdbcontext.Entry(newQuestionee).State = EntityState.Added;

                                AssociatedWordQuestionee newAssociatedWordQuestionee = new()
                                {
                                    AssociatedWord = newAssociatedWord,
                                    Questionee = newQuestionee,
                                };

                                await _tempdbcontext.AssociatedWordQuestionees.AddAsync(newAssociatedWordQuestionee);
                            }
                            else
                            {
                                associatedWord.Count += 1;

                                AssociatedWordQuestionee newAssociatedWordQuestionee = new()
                                {
                                    AssociatedWord = associatedWord,
                                    Questionee = newQuestionee,
                                };

                                // Attach the Questionee only once
                                if (!_tempdbcontext.Entry(newQuestionee).IsKeySet)
                                {
                                    _tempdbcontext.Entry(newQuestionee).State = EntityState.Added;
                                }

                                _tempdbcontext.AssociatedWord.Update(associatedWord);
                                await _tempdbcontext.AssociatedWordQuestionees.AddAsync(newAssociatedWordQuestionee);
                            }
                        }

                        await _tempdbcontext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                });
                #endregion

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetWordsFull")]
        public IActionResult GetWordsFull()
        {
            try
            {
                IEnumerable<WordDTO> words = _dbContext.Word
                    .Include(w => w.AssociatedWords)
                    .ThenInclude(aw => aw.AssociatedWordQuestionees)
                    .ThenInclude(aws => aws.Questionee)
                    .Select(w => new WordDTO
                    {
                        Id = w.Id,
                        Name = w.Name,

                        AssociatedWordDTOs = w.AssociatedWords
                            .Where(aw => aw.WordId == w.Id)
                            .Select(aw => new AssociatedWordDTO
                            {
                                Id = aw.Id,
                                Name = aw.Name,
                                Count = aw.Count,

                                QuestioneeDTOs = aw.AssociatedWordQuestionees
                                    .Where(aws => aws.AssociatedWordId == aw.Id)
                                    .Select(aws => new QuestioneeDTO
                                    {
                                        Id = aws.Questionee.Id,
                                        IsMan = aws.Questionee.IsMan,
                                        Age = aws.Questionee.Age,
                                    }),
                            }),
                    });

                return Ok(words);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWords")]
        public IActionResult GetWords()
        {
            try
            {
                IEnumerable<WordDTO> words = _dbContext.Word
                    .Select(w => new WordDTO
                    {
                        Id = w.Id,
                        Name = w.Name,
                    });

                return Ok(JsonConvert.SerializeObject(words));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetWordId")]
        public async Task<IActionResult> GetWordId([FromQuery] string word)
        {
            try
            {
                Word? foundWord = await _dbContext.Word.FirstOrDefaultAsync(w => w.Name.ToLower() == word);

                if (foundWord is null)
                    return BadRequest("No Word Found");

                return Ok(foundWord.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
