
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
                HostFactory.Run(x =>
                {
                    x.Service<MyService>(s =>
                    {
                        s.ConstructUsing(name => new MyService());
                        s.WhenStarted(tc => tc.Start());
                        s.WhenStopped(tc => tc.Stop());
                    });
                    x.RunAsLocalSystem();
                    var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                    x.SetDescription("Exentric PACS Support Tool");
                    x.SetDisplayName(assemblyName);
                    x.SetServiceName(assemblyName);
                });
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
