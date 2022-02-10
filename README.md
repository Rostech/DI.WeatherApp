# DI.WeatherApp
[SOLID] Dependency Inversion. What? How? 

![.net build workflow](https://github.com/rostech/DI.WeatherApp/actions/workflows/dotnet.yml/badge.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

### 🧑‍🎓 Disclaimer:
This summary on DI is based on my understanding of the DI principle (after research) and is for learning purposes. It's open for discussion and improvements. You can demo source code below.

# Dependency Inversion
## 🧠 Definition
#### High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should not depend on abstractions.

## The 🎯 of dependency inversion principle
Is to avoid this highly coupled distribution with the mediation of an abstract layer and to increase the re-usability of higher/policy layers. It promotes loosely coupled architecture, flexibility, pluggabiliy within our code.

## 🥇 Golden rule
High-level (client) modules should own the abstraction otherwise the dependency is not inverted!

## 🚀 Summary 
- **Dependency Inversion** **Principle**- Higher-level component owns the interface. Lower-level implements.
- **Dependency Injection** **Pattern** - Frequently, developers confuse IoC with DI. As mentioned previously, IoC deals with object creation being inverted, whereas DI is a pattern for supplying the dependencies since the control is inverted.
- **Inversion of Control** -  is the **technique** for inverting the control of object creation and **not the actual pattern for making it happen.**
- **Dependency Inversion** promotes loosely coupled architecture, flexibility, pluggability within our code
- Without **Dependency injection**, there is no Dependency inversion

## 🧰 Demo

I made this demo as a note to myself, say for future references, if/when I don't feel confident enough that I understand what **DI** is all about.
Over the years I've been reading about SOLID and DI all over the web, but I think it's best explained in simple N-layered architecture so I'll try doing just that. 

**DI.WheatherApp** is a simple demo project. It's organized like this:
<details>
  <summary>User Interface Layer</summary><blockquote>
  
  ### DI.WeatherApp.ConsoleClient
  A simple console client to display dummy weather data. This represents the UI layer and orchestrates the dependency injection. 
  
  <details>
    <summary>Startup.cs - Adds services to the DI container. Entry point for the console app. </summary><blockquote>
     
  ```
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
  ```
  <blockquote></details>  
    
  <details>
    <summary>WeatherDataConsumer.cs - simple console UI build with ConsoleTables and Humanize</summary><blockquote>
     
  ```
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
  ```
  <blockquote></details>  
    
    At the moment the UI is only referencing 
    <ProjectReference Include="..\DI.WeatherApp.Services\DI.WeatherApp.Services.csproj" />
<blockquote></details>

<details>
  <summary>Business Logic Layer</summary>
</details>

<details>
  <summary>Data access Layer</summary>
</details>
