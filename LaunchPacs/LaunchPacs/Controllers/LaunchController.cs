using LaunchPacs.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace LaunchPacs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaunchController : ControllerBase
    {

        private readonly ILogger<LaunchController> _logger;

        public LaunchController(ILogger<LaunchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get([FromQuery]G3LauncherModel g3LauncherModel)
        {
            // C:\INFINITT\viewer\G3Launcher.exe http://10.1.1.142+admin+nimda++S+RU799OR39MJ3BCF1+BET0001
            StringBuilder builderArgument= new StringBuilder();
            builderArgument.Append($"{g3LauncherModel.Iis}+");
            builderArgument.Append($"{g3LauncherModel.UserId}+");
            builderArgument.Append($"{g3LauncherModel.UserPassword}+");
            builderArgument.Append($"+S+");
            builderArgument.Append($"{g3LauncherModel.AccessionNo}+");
            builderArgument.Append($"{g3LauncherModel.PatientId}");
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
            using (Process proc = Process.Start(start))
            {
            }
            return command;
        }

        [HttpGet(template:"Version")]
        public string GetVersion()
        {
            var version = new Program().GetType().Assembly.GetName().Version!.ToString();
            var name = $"Exentric G3Launcher Support Tool (Version : {version})";
            return name;
        }
    }
}