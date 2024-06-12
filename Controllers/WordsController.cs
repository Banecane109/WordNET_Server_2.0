using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddAssociatedWord([FromBody] AssociatedWordDTO associatedWordDTO)
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
                        AssociatedWord? associatedWord = await _tempdbcontext.AssociatedWord.FirstOrDefaultAsync(aw => aw.Name == associatedWordDTO.Name);

                        if (associatedWord is null)
                        {
                            Statistics statistics = new()
                            {
                                ManCount = associatedWordDTO.IsMan is true ? 1 : 0,
                                ManAvarageAge = associatedWordDTO.IsMan is true ? associatedWordDTO.HumanAge : 0,

                                WomanCount = associatedWordDTO.IsMan is false ? 1 : 0,
                                WomanAvarageAge = associatedWordDTO.IsMan is false ? associatedWordDTO.HumanAge : 0,

                                AssociatedWordId = null,
                            };

                            _tempdbcontext.Statistics.Add(statistics);
                            await _tempdbcontext.SaveChangesAsync();

                            AssociatedWord createdAssociatedWord = new()
                            {
                                Name = associatedWordDTO.Name,
                                Count = 1,

                                WordId = word.Id,
                                StatisticsId = statistics.Id,
                            };

                            _tempdbcontext.AssociatedWord.Add(createdAssociatedWord);
                            await _tempdbcontext.SaveChangesAsync();

                            statistics.AssociatedWordId = createdAssociatedWord.Id;
                            await _tempdbcontext.SaveChangesAsync();
                        }
                        else
                        {
                            Statistics? existingStatistics = await _tempdbcontext.Statistics.FindAsync(associatedWord?.StatisticsId)
                                ?? throw new Exception("Required Statistics Are Not Found");

                            if (associatedWordDTO.IsMan is true)
                            {
                                existingStatistics.ManCount += 1;
                                existingStatistics.ManAvarageAge = (double)(existingStatistics.ManAvarageAge + associatedWordDTO.HumanAge) / existingStatistics.ManCount;

                                await _tempdbcontext.SaveChangesAsync();
                            }
                            else
                            {
                                existingStatistics.WomanCount += 1;
                                existingStatistics.WomanAvarageAge = (double)(existingStatistics.WomanAvarageAge + associatedWordDTO.HumanAge) / existingStatistics.WomanCount;

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
    }
}
