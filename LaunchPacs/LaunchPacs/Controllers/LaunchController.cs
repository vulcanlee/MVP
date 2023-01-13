using LaunchPacs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace LaunchPacs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaunchController : ControllerBase
    {

        private readonly ILogger<LaunchController> _logger;
        private readonly PacsConfiguration pacsConfiguration;

        public LaunchController(ILogger<LaunchController> logger,
            IOptions<PacsConfiguration> pacsOptions)
        {
            _logger = logger;
            this.pacsConfiguration = pacsOptions.Value;
        }

        [HttpGet]
        public string Get([FromQuery] G3LauncherModel g3LauncherModel)
        {
            #region 若沒有傳入，填入預設參數值
            if (string.IsNullOrEmpty(g3LauncherModel.Iis))
                g3LauncherModel.Iis = pacsConfiguration.IisUrl;
            if (string.IsNullOrEmpty(g3LauncherModel.ViewerPath))
                g3LauncherModel.ViewerPath = pacsConfiguration.PacsProgramPath;
            #endregion

                // C:\INFINITT\viewer\G3Launcher.exe http://10.1.1.142+admin+nimda++S+RU799OR39MJ3BCF1+BET0001
            StringBuilder builderArgument = new StringBuilder();
            builderArgument.Append($"{g3LauncherModel.Iis}+");
            builderArgument.Append($"{g3LauncherModel.LID}+");
            builderArgument.Append($"{g3LauncherModel.LPW}+");
            builderArgument.Append($"+S+");
            builderArgument.Append($"{g3LauncherModel.AN}+");
            builderArgument.Append($"{g3LauncherModel.PID}");
            string arguments = builderArgument.ToString();

            string command = $"{g3LauncherModel.ViewerPath} {arguments}";

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = arguments;
            // Enter the executable to run, including the complete path
            start.FileName = g3LauncherModel.ViewerPath;
            // Do you want to show a console window?
            //start.WindowStyle = ProcessWindowStyle.Hidden;
            //start.CreateNoWindow = true;
            var filename = Path.GetFileName(g3LauncherModel.ViewerPath);
            var path = g3LauncherModel.ViewerPath.Replace(filename, "");
            start.WorkingDirectory = path;
            start.CreateNoWindow = false;
            start.UseShellExecute = true;
            start.Verb = "runas";
            using (Process proc = Process.Start(start))
            {
            }
            return command;
        }

        [HttpGet(template: "Version")]
        public string GetVersion()
        {
            var version = new Program().GetType().Assembly.GetName().Version!.ToString();
            var name = $"Exentric G3Launcher Support Tool (Version : {version})";
            return name;
        }
    }
}