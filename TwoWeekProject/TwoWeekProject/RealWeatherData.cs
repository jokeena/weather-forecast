namespace TwoWeekProject
{
    public class RealWeatherData
    {
        public List<DailyWeather> Daily { get; set; }
    }

    public class DailyWeather
    {
        public DateOnly Date { get; set; }
        public double TemperatureMaxF { get; set; }
        public double TemperatureMinF { get; set; }
        public int WeatherCode { get; set; }
    }
}
