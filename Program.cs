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

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
