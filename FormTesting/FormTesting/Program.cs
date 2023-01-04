using TestingBusiness;
using TestingBusiness.Helpers;
using TestingBusiness.Services;
using TestingModel.Models;

namespace FormTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext,services) =>
                {
                    services.AddHostedService<Worker>();
                    services.Configure<TestingTargetConfiguration>(
                        hostContext.Configuration.GetSection("Target"));
                    services.Configure<List<TestingNodeConfiguration>>(
                        hostContext.Configuration.GetSection("TestingNodes"));
                    services.AddTransient<PerformanceMeasure>();
                    services.AddTransient<FormsStressTesting>();
                    services.AddTransient<FormService>();
                    services.AddTransient<RemotePerformanceService>();
                    services.AddSingleton<ConsoleHelper>();
                })
                .Build();

            host.Run();
        }
    }
}