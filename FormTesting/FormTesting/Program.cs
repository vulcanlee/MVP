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
                })
                .Build();

            host.Run();
        }
    }
}