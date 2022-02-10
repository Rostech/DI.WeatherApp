using System.Collections.Generic;

namespace DI.WeatherApp.Services
{
    public interface IDummyWeatherForecastRepository
    {
        /// <summary>
        /// Returns weather forecast data
        /// </summary>
        /// <returns></returns>
        IEnumerable<WeatherForecast> Get();
    }
}