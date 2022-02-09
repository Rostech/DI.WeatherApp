using System;

namespace DI.WeatherApp.Data
{
    public class WeatherForecastDbo
    {
        public string CityName { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
