using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WordNET_Server_2._0.DBRelations;

namespace WordNET_Server_2._0.Services.ExecutionService
{
    public interface IExecutionService
    {
        IDbTransaction BeginTransaction(DBContext _dbcontext);
        IExecutionStrategy BeginExecutionStrategy(DBContext _dbcontext);
    }
}
