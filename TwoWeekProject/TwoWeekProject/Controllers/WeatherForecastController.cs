using Microsoft.AspNetCore.Mvc;

namespace TwoWeekProject.Controllers
{
    [ApiController]
    [Route("[controller]")] //Replaced by controllers name minus controller. so weatherforecast. Called by WeatherService.ts
    public class WeatherForecastController : ControllerBase // inherits ControllerBase, which provides basic API controller functionality
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        // constructor for the controller, logger is injected by ASP.NET Cores dependecy injection, assigned private field
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        private string DetermineSummary(int tempF)
        {
            return tempF switch
            {
                <= 0 => "Freezing",
                <= 32 => "Bracing",
                <= 45 => "Chilly",
                <= 59 => "Cool",
                <= 68 => "Mild",
                <= 77 => "Warm",
                <= 86 => "Balmy",
                <= 95 => "Hot",
                <= 104 => "Sweltering",
                _ => "Scorching"
            };
        }

        private readonly Dictionary<string, string> _weatherEmojis = new()
    {
        { "Freezing", "\U0001F976" },
        { "Bracing", "\U0001F4A8" },
        { "Chilly", "\U0001F9CA" },
        { "Cool", "\U0001F60E" },
        { "Mild", "\U0001F31E" },
        { "Warm", "\U0001F31E" },
        { "Balmy", "\U0001F343" },
        { "Hot", "\U0001F525" },
        { "Sweltering", "\U0001F975" },
        { "Scorching", "\U0001F506" }
    };

        private readonly Dictionary<string, string> _temperatureColors = new()
    {
        { "Freezing", "#0066cc" },
        { "Bracing", "#4d9fff" },
        { "Chilly", "#66b3ff" },
        { "Cool", "#99ccff" },
        { "Mild", "#00cc00" },
        { "Warm", "#ffaa00" },
        { "Balmy", "#ff8800" },
        { "Hot", "#ff4400" },
        { "Sweltering", "#ff0000" },
        { "Scorching", "#cc0000" }
    };

        private string FormatDate(DateTime date)
        {
            var day = date.Day;
            var suffix = GetOrdinalSuffix(day);
            return $"{date:MMMM} {day}{suffix}, {date.Year}";
        }

        private string GetOrdinalSuffix(int day)
        {
            if (day > 3 && day < 21) return "th";
            return (day % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }

        private string GetSummaryEmoji(string summary)
            => _weatherEmojis.GetValueOrDefault(summary, "\u2753");

        private string GetTemperatureColor(string summary)
            => _temperatureColors.GetValueOrDefault(summary, "#333333");

        // Activities logic converted from TypeScript
        private string[] GetActivitiesForWeather(int tempF, string summary)
        {
            var lowerSummary = summary.ToLower();

            if (tempF >= 85)
                return new[] { "Swimming", "Beach volleyball", "Ice cream shopping", "Water parks", "Outdoor BBQ" };
            else if (tempF >= 70)
                return new[] { "Hiking", "Picnicking", "Outdoor sports", "Gardening", "Cycling" };
            else if (tempF >= 50)
                return new[] { "Walking", "Photography", "Outdoor markets", "Light jogging", "Sightseeing" };
            else if (tempF >= 32)
                return new[] { "Indoor activities", "Museums", "Coffee shops", "Shopping malls", "Movies" };
            else
            {
                if (lowerSummary.Contains("snow"))
                    return new[] { "Sledding", "Snowball fights", "Building snowmen", "Ice skating", "Hot chocolate" };
                return new[] { "Indoor games", "Reading", "Cozy cafes", "Board games", "Warm baths" };
            }
        }

        // Clothing logic converted from TypeScript
        private string[] GetClothingForWeather(int tempF, string summary)
        {
            var lowerSummary = summary.ToLower();

            if (tempF >= 85)
                return new[] { "T-shirt", "Shorts", "Sandals", "Sunglasses", "Hat" };
            else if (tempF >= 70)
                return new[] { "Light shirt", "Jeans/pants", "Sneakers", "Light jacket (evening)" };
            else if (tempF >= 50)
                return new[] { "Long sleeves", "Pants", "Light jacket", "Closed shoes" };
            else if (tempF >= 32)
                return new[] { "Sweater", "Warm jacket", "Long pants", "Boots", "Scarf" };
            else
            {
                if (lowerSummary.Contains("snow"))
                    return new[] { "Heavy coat", "Winter boots", "Gloves", "Warm hat", "Thermal layers" };
                return new[] { "Winter coat", "Warm layers", "Boots", "Gloves", "Beanie" };
            }
        }

        private (string descriptor, string iconClass, string color) GetFontAwesomeWeatherIcon(int tempF)
        {
            // 1/3 chance of precipitation
            bool hasPrecipitation = Random.Shared.Next(3) == 0;

            if (hasPrecipitation)
            {
                if (tempF <= 32)
                {
                    return ("Snow", "fa-solid fa-snowflake", "#74C0FC"); // Snow
                }
                else
                {
                    return Random.Shared.Next(2) == 0
                        ? ("Rain", "fa-solid fa-cloud-rain", "#74C0FC") // Rain
                        : ("Thunderstorms", "fa-solid fa-cloud-bolt", "#74C0FC");      // Thunderstorm
                }
            }
            else
            {
                var options = new[]
                {
                    ("Sunny","fa-solid fa-sun", "#FFD700"),         // Sunny
                    ("Cloudy","fa-solid fa-cloud", "#B0C4DE"),       // Cloudy
                    ("Partly Cloudy","fa-solid fa-cloud-sun", "#FFD700")    // Partly Sunny
                };
                return options[Random.Shared.Next(options.Length)];
            }
        }

        private (string descriptor, string iconClass, string color) GetFontAwesomeWeatherIconFromCode(int weatherCode)
{
    // Open-Meteo weathercode mapping: https://open-meteo.com/en/docs#api_form
    // Example mapping (expand as needed):
    return weatherCode switch
    {
        0 => ("Sunny", "fa-solid fa-sun", "#FFD700"), // Clear sky
        1 or 2 => ("Partly Cloudy", "fa-solid fa-cloud-sun", "#FFD700"), // Mainly clear, partly cloudy
        3 => ("Cloudy", "fa-solid fa-cloud", "#B0C4DE"), // Overcast
        45 or 48 => ("Foggy", "fa-solid fa-smog", "#B0C4DE"), // Fog
        51 or 53 or 55 or 56 or 57 => ("Rain", "fa-solid fa-cloud-rain", "#74C0FC"), // Drizzle
        61 or 63 or 65 or 66 or 67 => ("Rain", "fa-solid fa-cloud-rain", "#74C0FC"), // Rain
        71 or 73 or 75 or 77 or 85 or 86 => ("Snow", "fa-solid fa-snowflake", "#74C0FC"), // Snow
        80 or 81 or 82 => ("Rain", "fa-solid fa-cloud-showers-heavy", "#74C0FC"), // Rain showers
        95 or 96 or 99 => ("Thunderstorm", "fa-solid fa-cloud-bolt", "#FFD700"), // Thunderstorm
        _ => ("Unknown", "fa-solid fa-question", "#333333")
    };
}

        [HttpGet(Name = "GetWeatherForecast")] // marks method as handling HTTP GET requests.
        public async Task<IEnumerable<WeatherForecast>> Get() // public method named Get, returns a collection of WeatherForecast objects.
        {
            double latitude = 42.2808;
            double longitude = -83.7430;

            var dailyWeather = await GetNext7DaysWeatherAsync(latitude, longitude);

            // Use only the first 5 days
            return dailyWeather.Take(5).Select(dw =>
            {
                var tempF = (int)dw.TemperatureMaxF;
                var tempC = (int)((tempF - 32) * 0.5556);
                string summary = DetermineSummary(tempF);

                var (iconDescriptor, iconClass, iconColor) = GetFontAwesomeWeatherIconFromCode(dw.WeatherCode);

                return new WeatherForecast
                {
                    Date = dw.Date,
                    TemperatureC = tempC,
                    TemperatureF = tempF,
                    Summary = summary,
                    FormattedDate = FormatDate(dw.Date.ToDateTime(TimeOnly.MinValue)),
                    SummaryEmoji = GetSummaryEmoji(summary),
                    TemperatureColor = GetTemperatureColor(summary),
                    Activities = GetActivitiesForWeather(tempF, summary),
                    Clothing = GetClothingForWeather(tempF, summary),
                    WeatherIconDescriptor = iconDescriptor,
                    WeatherIconClass = iconClass,
                    WeatherIconColor = iconColor,
                };
            })
            .ToArray();
        }
        
        private async Task<List<DailyWeather>> GetNext7DaysWeatherAsync(double latitude, double longitude)
        {
            using var httpClient = new HttpClient();
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,weathercode&temperature_unit=fahrenheit&forecast_days=7&timezone=auto";
            var response = await httpClient.GetAsync(url);

            var dailyList = new List<DailyWeather>();

            if (!response.IsSuccessStatusCode)
                return dailyList;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("daily", out var daily))
            {
                var dates = daily.GetProperty("time");
                var maxTemps = daily.GetProperty("temperature_2m_max");
                var minTemps = daily.GetProperty("temperature_2m_min");
                var weatherCodes = daily.GetProperty("weathercode");

                for (int i = 0; i < dates.GetArrayLength(); i++)
                {
                    var date = DateOnly.Parse(dates[i].GetString()!);
                    var maxF = maxTemps[i].GetDouble();
                    var minF = minTemps[i].GetDouble();
                    var code = weatherCodes[i].GetInt32();

                    dailyList.Add(new DailyWeather
                    {
                        Date = date,
                        TemperatureMaxF = maxF,
                        TemperatureMinF = minF,
                        WeatherCode = code
                    });
                }
            }

            return dailyList;
        }
    }
}
