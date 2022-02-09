using System.Collections.Generic;
using System.Linq;
using DI.WeatherApp.Data;

namespace DI.WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IDummyWeatherForecastRepository weatherForecastRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService"/> class.
        /// </summary>
        /// <param name="weatherForecastRepository">The weather forecast repository.</param>
        public WeatherService(IDummyWeatherForecastRepository weatherForecastRepository)
        {
            this.weatherForecastRepository = weatherForecastRepository;
        }

        /// <inheritdoc/>
        public IEnumerable<WeatherForecast> Get()
        {
            return this.weatherForecastRepository.Get().Select(w => new WeatherForecast()
            {
                CityName = w.CityName,
                Date = w.Date,
                Summary = w.Summary,
                TemperatureC = w.TemperatureC
            });
        }
    }
}
