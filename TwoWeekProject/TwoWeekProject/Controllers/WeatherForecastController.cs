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

        private string GetRandomWeatherEmoji(int tempF)
        {
            // 1/3 chance of precipitation
            bool hasPrecipitation = Random.Shared.Next(3) == 0;

            if (hasPrecipitation)
            {
                if (tempF <= 32)
                {
                    return "\u2744";
                }
                else
                {
                    // Randomly pick rain or thunderstorm
                    return Random.Shared.Next(2) == 0 ? "\U0001F327" : "\u26C8";
                }
            }
            else
            {
                // Randomly pick sunny, cloudy, or partly sunny (all colored)
                var options = new[] { "\U0001F31E", "\U0001F324", "\U0001F325" };
                return options[Random.Shared.Next(options.Length)];
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

        [HttpGet(Name = "GetWeatherForecast")] // marks method as handling HTTP GET requests.
        public IEnumerable<WeatherForecast> Get() // public method named Get, returns a collection of WeatherForecast objects.
        {
            return Enumerable.Range(1, 5).Select(index =>
            {
                var tempC = Random.Shared.Next(-20, 55);
                var tempF = 32 + (int)(tempC / 0.5556); // calculate Fahrenheit for summary selection

                // choose summary based on temperature in Fahrenheit
                string summary = DetermineSummary(tempF);

                var date = DateTime.Now.AddDays(index); // Fix: Declare a local variable 'date' to hold the DateTime value.

                var (iconDescriptor, iconClass, iconColor) = GetFontAwesomeWeatherIcon(tempF);

            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(date),
                                TemperatureC = tempC,
                                Summary = summary,
                                FormattedDate = FormatDate(date), // Fix: Use the local 'date' variable here.
                                SummaryEmoji = GetSummaryEmoji(summary),
                                TemperatureColor = GetTemperatureColor(summary),
                                Activities = GetActivitiesForWeather(tempF, summary),
                                Clothing = GetClothingForWeather(tempF, summary),
                                WeatherEmoji = GetRandomWeatherEmoji(tempF),
                                WeatherIconDescriptor = iconDescriptor,
                                WeatherIconClass = iconClass,
                                WeatherIconColor = iconColor,
                            };
                        })
                        .ToArray();
                        // generates 5 WeatherForecast objects, returned as an array. For each number a new WeatherForecast is created, todays data plus index days, random tempC, and a summary that matches the temperature.
                    }
                }
            }
