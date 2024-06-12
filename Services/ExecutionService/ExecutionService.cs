using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WordNET_Server_2._0.DBRelations;

namespace WordNET_Server_2._0.Services.ExecutionService
{
    public class ExecutionService : IExecutionService
    {
        public IDbTransaction BeginTransaction(DBContext _dbcontext) => _dbcontext.Database.BeginTransaction().GetDbTransaction();
        public IExecutionStrategy BeginExecutionStrategy(DBContext _dbcontext) => _dbcontext.Database.CreateExecutionStrategy();
    }
}
