using DI.WeatherApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace DI.WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IDummyWeatherForecastRepository weatherForecastRepository;

        public WeatherService(IDummyWeatherForecastRepository weatherForecastRepository)
        {
            this.weatherForecastRepository = weatherForecastRepository;
        }

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
