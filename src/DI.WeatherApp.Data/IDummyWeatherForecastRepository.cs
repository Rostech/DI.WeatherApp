using System.Collections.Generic;

namespace DI.WeatherApp.Data
{
    public interface IDummyWeatherForecastRepository
    {
        /// <summary>
        /// Returns weather forecast data
        /// </summary>
        /// <returns></returns>
        IEnumerable<WeatherForecastDbo> Get();
    }
}