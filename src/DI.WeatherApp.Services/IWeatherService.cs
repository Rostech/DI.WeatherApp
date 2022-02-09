using System.Collections.Generic;

namespace DI.WeatherApp.Services
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> Get();
    }
}