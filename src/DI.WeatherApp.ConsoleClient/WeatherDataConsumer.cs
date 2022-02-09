using ConsoleTables;
using DI.WeatherApp.Services;
using Humanizer;

namespace DI.WeatherApp.ConsoleClient
{
    public class WeatherDataConsumer
    {
        private readonly IWeatherService weatherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherDataConsumer"/> class.
        /// </summary>
        /// <param name="weatherService">The weather service.</param>
        public WeatherDataConsumer(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        /// <summary>
        /// Displays data on the console with the ConsoleTable and Humanize libraries
        /// </summary>
        public void Display()
        {
            var table = new ConsoleTable(
                nameof(WeatherForecast.CityName).Humanize(),
                nameof(WeatherForecast.Date).Humanize(),
                nameof(WeatherForecast.TemperatureC).Humanize(),
                nameof(WeatherForecast.TemperatureF).Humanize(),
                nameof(WeatherForecast.Summary).Humanize());

            foreach (var forecast in this.weatherService.Get())
            {
                table.AddRow(forecast.CityName,
                    forecast.Date.ToString("ddd, dd MMM yyy"),
                    forecast.TemperatureC,
                    forecast.TemperatureF,
                    forecast.Summary);
            }

            table.Write();
        }
    }
}
