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
        public async Task<IActionResult> AddAssociatedWord([FromBody] UserAssociatedWordDTO associatedWordDTO)
        {
            try
            {
                #region Initial Check
                Word? word = await _dbContext.Word.FindAsync(associatedWordDTO.WordId);

                if (word is null)
                    return BadRequest("Specified Word Not Found In Database");
                #endregion

                #region Adding Associated Word
                using DBContext _tempdbcontext = new();

                var executionStrategy = _executionService.BeginExecutionStrategy(_tempdbcontext);
                await executionStrategy.ExecuteAsync(async () =>
                {
                    using var transaction = _executionService.BeginTransaction(_tempdbcontext);

                    try
                    {
                        AssociatedWord? associatedWord = await _tempdbcontext.AssociatedWord
                            .Include(aw => aw.Statistics)
                            .FirstOrDefaultAsync(aw => aw.Name == associatedWordDTO.Name && aw.WordId == associatedWordDTO.WordId);

                        if (associatedWord is null)
                        {
                            Statistics createdStatistics = new()
                            {
                                ManCount = associatedWordDTO.IsMan is true ? 1 : 0,
                                ManAverageAge = associatedWordDTO.IsMan is true ? associatedWordDTO.HumanAge : 0,

                                WomanCount = associatedWordDTO.IsMan is false ? 1 : 0,
                                WomanAverageAge = associatedWordDTO.IsMan is false ? associatedWordDTO.HumanAge : 0,
                            };

                            AssociatedWord createdAssociatedWord = new()
                            {
                                Name = associatedWordDTO.Name,
                                Count = 1,

                                WordId = word.Id,
                                Statistics = createdStatistics,
                            };

                            _tempdbcontext.AssociatedWord.Add(createdAssociatedWord);
                            await _tempdbcontext.SaveChangesAsync();

                            createdStatistics.AssociatedWordId = createdAssociatedWord.Id;
                            createdAssociatedWord.StatisticsId = createdStatistics.Id;
                            await _tempdbcontext.SaveChangesAsync();
                        }
                        else
                        {
                            if (associatedWordDTO.IsMan is true)
                            {
                                associatedWord.Statistics.ManCount += 1;
                                associatedWord.Statistics.ManAverageAge = (double)(associatedWord.Statistics.ManAverageAge + associatedWordDTO.HumanAge) / associatedWord.Statistics.ManCount;

                                await _tempdbcontext.SaveChangesAsync();
                            }
                            else
                            {
                                associatedWord.Statistics.WomanCount += 1;
                                associatedWord.Statistics.WomanAverageAge = (double)(associatedWord.Statistics.WomanAverageAge + associatedWordDTO.HumanAge) / associatedWord.Statistics.WomanCount;

                                await _tempdbcontext.SaveChangesAsync();
                            }

                            associatedWord.Count += 1;
                            await _tempdbcontext.SaveChangesAsync();
                        }

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

        [HttpGet("GetWords")]
        public IActionResult GetWords()
        {
            try
            {
                IEnumerable<WordDTO> words = _dbContext.Word
                    .Include(w => w.AssociatedWords)
                    .ThenInclude(aw => aw.Statistics)
                    .Select(w => new WordDTO
                    {
                        Id = w.Id,
                        Name = w.Name,

                        AssociatedWords = w.AssociatedWords.Select(aw => new AssociatedWordDTO
                        {
                            Id = aw.Id,
                            Name = aw.Name,
                            Count = aw.Count,

                            Statistics = new StatisticsDTO
                            {
                                Id = aw.Statistics.Id,

                                ManCount = aw.Statistics.ManCount,
                                ManAverageAge = aw.Statistics.ManAverageAge,

                                WomanCount = aw.Statistics.WomanCount,
                                WomanAverageAge = aw.Statistics.WomanAverageAge,

                                AssociatedWordId = aw.Id,
                            },

                            StatisticsId = aw.Statistics.Id,
                            WordId = w.Id,
                        }),
                    });

                return Ok(JsonConvert.SerializeObject(words));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
