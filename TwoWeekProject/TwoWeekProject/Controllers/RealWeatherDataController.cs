using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TwoWeekProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RealWeatherDataController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<RealWeatherData>> Get(double latitude = 40.7128, double longitude = -74.0060)
        {
            using var httpClient = new HttpClient();
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min&temperature_unit=fahrenheit&forecast_days=7&timezone=auto";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Failed to fetch weather data.");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            var dailyList = new List<DailyWeather>();

            if (root.TryGetProperty("daily", out var daily))
            {
                var dates = daily.GetProperty("time");
                var maxTemps = daily.GetProperty("temperature_2m_max");
                var minTemps = daily.GetProperty("temperature_2m_min");

                for (int i = 0; i < dates.GetArrayLength(); i++)
                {
                    var date = DateOnly.Parse(dates[i].GetString()!);
                    var maxF = maxTemps[i].GetDouble();
                    var minF = minTemps[i].GetDouble();

                    dailyList.Add(new DailyWeather
                    {
                        Date = date,
                        TemperatureMaxF = maxF,
                        TemperatureMinF = minF
                    });
                }
            }

            return new RealWeatherData
            {
                Daily = dailyList
            };
        }
    }
}