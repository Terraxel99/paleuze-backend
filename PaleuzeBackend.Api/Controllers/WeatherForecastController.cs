using Microsoft.AspNetCore.Mvc;
using PaleuzeBackend.Business;

namespace PaleuzeBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Class1.Summaries[Random.Shared.Next(Class1.Summaries.Length)]
            })
            .ToArray();
        }
    }
}
