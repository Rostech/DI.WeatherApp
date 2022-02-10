using System.Collections.Generic;

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
            return this.weatherForecastRepository.Get();
        }
    }
}
