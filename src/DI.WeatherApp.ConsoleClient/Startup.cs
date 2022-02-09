using System.Threading.Tasks;
using DI.WeatherApp.Data;
using DI.WeatherApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DI.WeatherApp.ConsoleClient
{
    class Startup
    {
        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services.AddScoped<IWeatherService, WeatherService>()
                    .AddScoped<IDummyWeatherForecastRepository, DummyWeatherForecastRepository>()
                    .AddScoped<WeatherDataConsumer>());

        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var weatherDataConsumer = host.Services.GetRequiredService<WeatherDataConsumer>();
            weatherDataConsumer.Display();

            await host.RunAsync();
        }
    }
}
