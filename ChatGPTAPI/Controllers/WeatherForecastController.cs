using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;

namespace ChatGPTAPI.Controllers
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

        [HttpGet()]
        [Route(template:"GetWeather")]
        public async Task<IActionResult> Get(Kernel kernal)
        {
            var temp = Random.Shared.Next(-20, 55);
            var result= new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = temp,
                //Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                Summary= await kernal.InvokePromptAsync<string>(promptTemplate: $"please provide a short description of the temp {temp} C")
            };

            return Ok(result);
        }
    }
}
