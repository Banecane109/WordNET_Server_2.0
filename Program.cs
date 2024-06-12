using WordNET_Server_2._0.DBRelations;
using WordNET_Server_2._0.Services.ExecutionService;

namespace WordNET_Server_2._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient();

            builder.Services.AddTransient<DBContext>();
            builder.Services.AddTransient<IExecutionService, ExecutionService>();

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
