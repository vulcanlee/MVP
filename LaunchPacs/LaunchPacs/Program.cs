
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
            #region 刪除掉還在執行的同樣程式碼
            var processes = Process.GetProcesses().ToList();
            var LaunchPacsProcesses = processes.Where(x => x.ProcessName.ToLower().Contains("LaunchPacs".ToLower())).ToList();
            var currentProcess = Process.GetCurrentProcess();
            foreach (var item in LaunchPacsProcesses)
            {
                if (item.Id != currentProcess.Id)
                {
                    Console.WriteLine($"Process {item.Id} will be killed!");
                    item.Kill();
                }
            }
            #endregion

            #region 是否需要立即結束
            if (args.Length > 0)
            {
                foreach (var item in args)
                {
                    if (item.ToLower() == "quit")
                    {
                        return;
                    }
                }
            }
            #endregion

            #region 是否要重新啟動為隱藏視窗模式
            if (args.Length > 0)
            {
                foreach (var item in args)
                {
                    if (item.ToLower() == "hide")
                    {
                        string basePath = Directory.GetCurrentDirectory();
                        string filename = "LaunchPacs.exe";
                        string exeFilename = Path.Combine(basePath, filename);
                        System.Diagnostics.ProcessStartInfo start =
                            new System.Diagnostics.ProcessStartInfo();
                        start.FileName = exeFilename;
                        start.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Hides GUI
                        start.CreateNoWindow = true; //Hides console
                        Process.Start(start);
                        return;
                    }
                }
            }
            #endregion

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

                string MyAllowAllOrigins = "All";
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowAllOrigins,
                                          policy =>
                                          {
                                              policy.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                                          });
                });

                var App = builder.Build();

                #region 是否需要隱藏此命令字元視窗
                PacsConfiguration pacsConfiguration = App
                    .Services.GetService<IOptions<PacsConfiguration>>().Value;
                if (pacsConfiguration.HiddenWindown)
                {
                    var handle = GetConsoleWindow();
                    ShowWindow(handle, SW_HIDE);
                }
                #endregion

                // Configure the HTTP request pipeline.
                if (App.Environment.IsDevelopment())
                {
                    App.UseSwagger();
                    App.UseSwaggerUI();
                }

                // app.UseHttpsRedirection();

                App.UseCors(MyAllowAllOrigins);

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
