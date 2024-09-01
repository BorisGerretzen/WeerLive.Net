# WeerLive.Net
[![NuGet](https://img.shields.io/nuget/v/WeerLive.svg)](https://www.nuget.org/packages/WeerLive/)

WeerLive.Net is a library for the Dutch weather service [WeerLive](https://weerlive.nl/delen.php), the data comes
directly from the 10-minutes network.
This library provides the following features:

- Get the live weather for a location
- Get the weather forecast for the coming 24 hours for a location
- Get the weather forecast for the coming 5 days for a location

The location can be either a city name or latitude and longitude coordinates.

## Usage

Here you can see a simple example of how to use the library combined with the Microsoft.Extensions.DependencyInjection
library.
This example can also be found in
the [example project](https://github.com/BorisGerretzen/WeerLive.Net/tree/main/src/WeerLive.Example).

```csharp
var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
    services.AddWeerLive(conf => ctx.Configuration.GetSection("WeerLive").Bind(conf)));
var host = builder.Build();

var serviceScope = host.Services.CreateScope();
var client = serviceScope.ServiceProvider.GetRequiredService<IWeerLiveClient>();
var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();

var forecast = await client.GetAsync("Amsterdam");
if (forecast.LiveWeather is null)
{
    logger.LogError("Failed to get weather forecast.");
    return;
}

logger.LogInformation("Today in Amsterdam: {Temperature}°C with a RH of {Humidity}%.",
    forecast.LiveWeather.Temperature,
    forecast.LiveWeather.Humidity);
```

And without dependency injection:

```csharp
var httpClient = new HttpClient();
var options = new WeerLiveOptions
{
    ApiKey = "demo"
};

var client = new WeerLiveClient(httpClient, Options.Create(options));
var forecast = await client.GetAsync("Amsterdam");
if (forecast.LiveWeather is null)
{
    Console.WriteLine("Failed to get weather forecast.");
    return;
}

Console.WriteLine($"Today in Amsterdam: {forecast.LiveWeather.Temperature}°C with a RH of {forecast.LiveWeather.Humidity}%.");
```

## API key

To use the WeerLive API, you need an API key. You can get a free API key by registering on
the [WeerLive website](https://weerlive.nl/delen.php).
With an API key you can make up to 300 requests per day. In the responses you can find how many requests you have left
for the day.
