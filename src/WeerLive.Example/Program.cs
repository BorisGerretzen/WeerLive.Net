using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeerLive.Lib;
using WeerLive.Lib.Client;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
    services.AddWeerLive(conf => ctx.Configuration.GetSection("WeerLive").Bind(conf)));
var host = builder.Build();

var serviceScope = host.Services.CreateScope();
var client = serviceScope.ServiceProvider.GetRequiredService<IWeerLiveClient>();
var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();

var forecast = await client.GetAsync("Amsterdam");
if (forecast?.LiveWeather is null)
{
    logger.LogError("Failed to get weather forecast.");
    return;
}

logger.LogInformation("Today in Amsterdam: {Temperature}°C with a RH of {Humidity}%.",
    forecast.LiveWeather.Temperature,
    forecast.LiveWeather.Humidity);