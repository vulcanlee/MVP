using Microsoft.AspNetCore.Mvc;

namespace LaunchPacs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaunchController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public LaunchController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "OK";
        }

        [HttpGet(template:"Version")]
        public string GetVersion()
        {
            var version = new Program().GetType().Assembly.GetName().Version!.ToString();
            return version;
        }
    }
}