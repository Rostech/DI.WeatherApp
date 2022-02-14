# DI.WeatherApp
[SOLID] Dependency Inversion. What? How? 

![.net build workflow](https://github.com/rostech/DI.WeatherApp/actions/workflows/dotnet.yml/badge.svg)
[![License: MIT](https://img.shields.io/badge/License-MIT-red.svg)](https://opensource.org/licenses/MIT)
[![Generic badge](https://img.shields.io/badge/Read-8min-blue.svg)]()

### 🧑‍🎓 Disclaimer:
This summary on DI is based on my understanding of the DI principle (after research) and is for learning purposes. It's open for discussion and improvements. You can check out the demo source code below.

# Dependency Inversion
## 🧠 Definition
#### High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should not depend on abstractions.

## The 🎯 of dependency inversion principle
Is to avoid this highly coupled distribution with the mediation of an abstract layer and to increase the re-usability of higher/policy layers. It promotes loosely coupled architecture, flexibility, pluggability within our code.

## 🥇 Golden rule
High-level (client) modules should own the abstraction otherwise the dependency is not inverted!

## 🚀 Summary 
- **Dependency Inversion** **Principle**- Higher-level component owns the interface. Lower-level implements.
- **Dependency Injection** **Pattern** - Frequently, developers confuse IoC with DI. As mentioned previously, IoC deals with object creation being inverted, whereas DI is a pattern for supplying the dependencies since the control is inverted.
- **Inversion of Control** -  is the **technique** for inverting the control of object creation and **not the actual pattern for making it happen.**
- **Dependency Inversion** promotes loosely coupled architecture, flexibility, pluggability within our code
- Without **Dependency injection**, there is no Dependency inversion

## 🧰 Demo

This demo is for future references if/when I don't feel confident enough that I understand in practice what **DI** is all about.
I think dependency inversion is best explained in simple N-layered architecture so I'll try doing just that. 

![console-ui](https://user-images.githubusercontent.com/10576276/153451888-c6f22f1b-15fe-4fd9-99cd-3db2cfad4569.png)


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
  <summary>Business Logic Layer</summary><blockquote>
  
  ### DI.WeatherApp.Services
  This represents the business layer.
  
  <details>
      <summary>WeatherService.cs - weather service that uses the dummy weather forecast repository to return data</summary><blockquote>
     
  ```
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
  ```
  <blockquote></details>  
    
  <details>
    <summary>IWeatherService.cs - abstraction over the WeatherService class</summary><blockquote>  
  <blockquote></details>
  <details>
    <summary>WeatherForecast.cs - POCO holding weather data</summary><blockquote>
    
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets the temperature Fahrenheit.
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }
    }
    
   <blockquote></details> 
    
    
    At the moment the Business Layer is only referencing 
    <ProjectReference Include="..\DI.WeatherApp.Services\DI.WeatherApp.Data.csproj" />
<blockquote></details>    
    
    


<details>
  <summary>Data access Layer</summary><blockquote>
  
  ### DI.WeatherApp.Data
  Represents the data access layer.
  
  <details>
    <summary>DummyWeatherForecastRepository.cs - dummy weather data repository</summary><blockquote>  
    
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
    
  <blockquote></details>
  
  <details>
    <summary>IDummyWeatherForecastRepository.cs - abstraction over the DummyWeatherForecastRepository class</summary><blockquote>  
  <blockquote></details>
  
  <details>  
    <summary>WeatherForecastDbo.cs - Weather Data Dbo</summary><blockquote>  
    
    public class WeatherForecastDbo
    {
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }
    }
    
  <blockquote></details>
    
<blockquote></details>
    
 #### Altho we have some dependency injection in this setup, we are not inverting any dependencies thus not implementing the dependency inversion principle.
    
![not-inverted-dependency](https://user-images.githubusercontent.com/10576276/153411510-6b83e74d-1210-465c-a5f6-4f32c2e76a97.png)

As shown in the graph, the dependency is not inverted and the UI is depending on the Business layer, which depends on Data layer. 
This means that the higher-level layer depends on the implementation details of the lower-level layer. We're trying to use an interface to decouple this dependency but this is not enough. 
### So what?!
To demonstrate an issue with not inverting dependencies we could introduce a change in the Data layer. Let's say that we need to change a property name in the ```WeatherForecastDbo.cs```
    
![changed-property-name](https://user-images.githubusercontent.com/10576276/153416701-2c656ac5-5f45-4c31-a51d-050ddc5712d8.png)

 ### When we build the project we get the following errors.

![build-errors](https://user-images.githubusercontent.com/10576276/153421063-b8fcbfa1-3f7f-498f-b02c-042df4531c62.png)
    
 The issue here is that this error is now reflecting an outside project in the business layer. This means, that if we need to rename a property in our database, this would affect the business logic. We could think, that after we're using an interface (abstraction) we're safe from such things, but we didn't actually invert the dependencies. 

To fix this, we need to follow the simple rule - the client (higher modules) own the interface.
    
1. ### ✔️ Change interface ownership. 
    The IDummyWeatherForecastRepository should be owned by the DI.WeatherApp.Services instead of the DI.WeatherApp.Data
    Also the ```IEnumerable<WeatherForecastDbo> Get(); ``` should now return ``` IEnumerable<WeatherForecast> Get(); ``` because this is all we need in the business layer.
    
    ![change-interface-ownership](https://user-images.githubusercontent.com/10576276/153426030-1846bd75-2470-4a4f-a34e-96066b18ff54.png)
    
2. ### ✔️ Invert the dependency. 
    Remove project reference to ```DI.WeatherApp.Data``` from ```DI.WeatherApp.Services``` and add a reference to ```DI.WeatherApp.Services``` in ```DI.WeatherApp.Data``` to start using the ```IDummyWeatherForecastRepository``` interface. We also need to move the ```WeatherForecastDbo``` mapping to WeatherForecast in the ```DummyWeatherForecastRepository``` because now it is responsible for returning ```WeatherForecast``` data.

    ![invert-project-dependencies](https://user-images.githubusercontent.com/10576276/153431201-b2077d3a-b322-45f8-8f2d-8f6e07d1c6ba.png)

    . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
    
    ![interface-implementation](https://user-images.githubusercontent.com/10576276/153431241-b410afc9-c757-4194-a76f-57b179e392c2.png)
    
3. ### ✔️ Register all dependencies in the UI layer.
    In our case, the UI layer is the composition route for all dependencies.
    
    ![register-data-layer-in-ui](https://user-images.githubusercontent.com/10576276/153446989-e5250e3b-b216-4788-a409-5817b6b615fc.png)
    
    ### ⚖️ Without dependency injection we can not achieve dependency inversion.
    
    ![dependency-injection-configuration](https://user-images.githubusercontent.com/10576276/153447553-e25d72e6-5e47-4245-ba6c-d3484161ebd0.png)


### 🎉 Did we invert the dependency? 🎉

To verify that dependency was indeed inverted, we will introduce a change in the Data layer like before. 
🚀 This time the error will be contained only in the ```DI.WeatherApp.Data``` and we don't need to update higher-level modules.

![inverted-dependency](https://user-images.githubusercontent.com/10576276/153450365-a9be6cf7-942d-4ea0-ac14-fed620f9855a.png)

    
### This is what the dependency flow looks like
    
![inverted-dependency-flow-chart](https://user-images.githubusercontent.com/10576276/153450900-c7c486be-ed46-4bce-8071-ee277909a320.png)
    
## 🎉 We can make changes to the lower level DI.WeatherApp.Data project, and this won't affect higher 
    
🔗 [Check out this PR to see what was changed so that the project is following dependency inversion principle](https://github.com/Rostech/DI.WeatherApp/pull/5/commits)
