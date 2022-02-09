using System;

namespace DI.WeatherApp.Data
{
    public class WeatherForecastDbo
    {
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }
    }
}
