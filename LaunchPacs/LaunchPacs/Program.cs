
using LaunchPacs.Models;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Web;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Topshelf;

namespace LaunchPacs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region 隱藏視窗會用到的 Windows API 宣告
            [DllImport("kernel32.dll")]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            #endregion

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

                builder.Services.Configure<PacsConfiguration>(
                    builder.Configuration.GetSection("PACS"));

                var App = builder.Build();

                #region 是否需要隱藏此命令字元視窗
                PacsConfiguration pacsConfiguration = App
                    .Services.GetService<IOptions<PacsConfiguration>>().Value;
                if(pacsConfiguration.HiddenWindown)
                {
                    //var handle = GetConsoleWindow();
                    //// Hide
                    //ShowWindow(handle, SW_HIDE);

                    IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                    ShowWindow(h, 0);
                }
                #endregion

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
