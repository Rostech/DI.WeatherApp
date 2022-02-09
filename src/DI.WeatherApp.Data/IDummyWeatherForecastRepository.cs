using System.Collections.Generic;

namespace DI.WeatherApp.Data
{
    public interface IDummyWeatherForecastRepository
    {
        IEnumerable<WeatherForecastDbo> Get();
    }
}