using System.Collections.Generic;

namespace DI.WeatherApp.Services
{
    public interface IWeatherService
    {
        /// <summary>
        /// Gets weather forecast data.
        /// </summary>
        /// <returns></returns>
        IEnumerable<WeatherForecast> Get();
    }
}