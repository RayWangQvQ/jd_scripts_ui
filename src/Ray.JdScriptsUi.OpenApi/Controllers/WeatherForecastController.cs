using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Ray.JdScriptsUi.OpenApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("test")]
        [HttpGet]
        public async Task<string> Test()
        {
            var re = "";

            IFileProvider fileProvider = new PhysicalFileProvider(@"G:\DockerContainers\jd_scripts\jd_scripts_bak");

            IFileInfo fileInfo = fileProvider.GetFileInfo("index.js");

            using (StreamReader readSteam = new StreamReader(fileInfo.CreateReadStream()))
            {
                re = await readSteam.ReadToEndAsync();
            }

            return re;
        }
    }
}
