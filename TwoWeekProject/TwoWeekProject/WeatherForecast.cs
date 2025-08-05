namespace TwoWeekProject
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public string FormattedDate { get; set; }
        public string SummaryEmoji { get; set; }
        public string TemperatureColor { get; set; }

        public string[] Activities { get; set; }
        public string[] Clothing { get; set; }
    }
}
