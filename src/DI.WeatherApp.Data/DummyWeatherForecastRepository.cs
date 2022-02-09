using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.WeatherApp.Data
{
    public class DummyWeatherForecastRepository : IDummyWeatherForecastRepository
    {
        #region Private fields

        private static readonly string[] Summaries = new[]
        {
            "Warm", "Bring an umbrella", "Chilly", "Freezing"
        };

        private static readonly int[] Temperatures = new[]
        {
            20, 10, 5, -4
        };

        private static readonly string[] CityNames = new[]
        {
            "Sofia", "London", "New York", "Brisbane", "Novosibirsk"
        };

        #endregion

        /// <inheritdoc/>
        public IEnumerable<WeatherForecastDbo> Get()
        {
            var random = new Random();

            return Enumerable.Range(1, CityNames.Length - 1)
                .Select(i =>
                {
                    var randomIndex = random.Next(Summaries.Length);

                    return new WeatherForecastDbo
                    {
                        CityName = CityNames[i],
                        Date = DateTime.Now.AddDays(1),
                        Summary = Summaries[randomIndex],
                        TemperatureC = Temperatures[randomIndex]
                    };
                })
                .ToArray();
        }
    }
}
