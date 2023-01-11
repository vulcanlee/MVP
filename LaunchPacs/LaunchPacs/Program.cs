
using NLog;
using NLog.Web;
using System.Reflection;
using Topshelf;

namespace LaunchPacs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup()
                .LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Host.UseNLog();

                var App = builder.Build();

                // Configure the HTTP request pipeline.
                if (App.Environment.IsDevelopment())
                {
                    App.UseSwagger();
                    App.UseSwaggerUI();
                }

                // app.UseHttpsRedirection();

                App.UseAuthorization();


                App.MapControllers();

                App.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}
