using Microsoft.Extensions.Options;
using Moq;
using RichardSzalay.MockHttp;
using WeerLive.Lib;
using WeerLive.Lib.Client;

namespace WeerLive.Test;

public class WeerLiveTests
{
    [Test]
    public async Task GetAsync_DemoLocation()
    {
        var httpClient = MockClient("?key=demo&locatie=Amsterdam", DemoResponse);
        var options = Options.Create(new WeerLiveOptions { ApiKey = "demo" });
        var client = new WeerLiveClient(httpClient, options);

        var response = await client.GetAsync("Amsterdam");

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response!.LiveWeatherList, Has.Count.EqualTo(1));
            Assert.That(response.LiveWeather!.Place, Is.EqualTo("Amsterdam"));
            Assert.That(response.LiveWeather.Temperature, Is.EqualTo(18.9));
            Assert.That(response.LiveWeather.TemperaturePerceived, Is.EqualTo(17.3));
            Assert.That(response.LiveWeather.Summary, Is.EqualTo("Licht bewolkt"));
            Assert.That(response.LiveWeather.Humidity, Is.EqualTo(71));
            Assert.That(response.LiveWeather.WindDirection, Is.EqualTo("ONO"));
            Assert.That(response.LiveWeather.WindDirectionDegrees, Is.EqualTo(62.1));
            Assert.That(response.LiveWeather.WindSpeedMs, Is.EqualTo(6.33));
            Assert.That(response.LiveWeather.WindSpeedBft, Is.EqualTo(4));
            Assert.That(response.LiveWeather.WindSpeedKn, Is.EqualTo(12.3));
            Assert.That(response.LiveWeather.WindSpeedKmh, Is.EqualTo(22.8));
            Assert.That(response.LiveWeather.AirPressure, Is.EqualTo(1021.51));
            Assert.That(response.LiveWeather.AirPressureMmHg, Is.EqualTo(766));
            Assert.That(response.LiveWeather.DewPoint, Is.EqualTo(13.6));
            Assert.That(response.LiveWeather.Visibility, Is.EqualTo(30400));
            Assert.That(response.LiveWeather.SolarIrradiance, Is.EqualTo(0));
            Assert.That(response.LiveWeather.DayForecast,
                Is.EqualTo("Vrij zonnig en vrijwel overal droog. Zondag in het zuiden tropisch warm"));
            Assert.That(response.LiveWeather.Sunrise, Is.EqualTo("06:49"));
            Assert.That(response.LiveWeather.Sunset, Is.EqualTo("20:32"));
            Assert.That(response.LiveWeather.Image, Is.EqualTo("wolkennacht"));
            Assert.That(response.LiveWeather.WeatherAlert, Is.EqualTo(0));
            Assert.That(response.LiveWeather.WeatherAlertHeader, Is.EqualTo("Er zijn geen waarschuwingen"));
            Assert.That(response.LiveWeather.WeatherAlertDescription,
                Is.EqualTo(" Er zijn momenteel geen waarschuwingen van kracht."));
            Assert.That(response.LiveWeather.WarningColor, Is.EqualTo("groen"));
            Assert.That(response.LiveWeather.WeatherAlertStart, Is.EqualTo("-"));
            Assert.That(response.LiveWeather.WeatherAlertTimestamp, Is.EqualTo(0));
            Assert.That(response.LiveWeather.WeatherAlertColorCode, Is.EqualTo("-"));
        });

        Assert.Multiple(() =>
        {
            Assert.That(response.WeekForecast, Has.Count.EqualTo(5));
            Assert.That(response.WeekForecast[0].Day, Is.EqualTo("31-08-2024"));
            Assert.That(response.WeekForecast[0].Image, Is.EqualTo("halfbewolkt"));
            Assert.That(response.WeekForecast[0].MaxTemperature, Is.EqualTo(19));
            Assert.That(response.WeekForecast[0].MinTemperature, Is.EqualTo(19));
            Assert.That(response.WeekForecast[0].WindSpeedBft, Is.EqualTo(3));
            Assert.That(response.WeekForecast[0].WindSpeedKmh, Is.EqualTo(18));
            Assert.That(response.WeekForecast[0].WindSpeedKn, Is.EqualTo(10));
            Assert.That(response.WeekForecast[0].WindSpeedMs, Is.EqualTo(5));
            Assert.That(response.WeekForecast[0].WindDirectionDegrees, Is.EqualTo(67));
            Assert.That(response.WeekForecast[0].WindDirection, Is.EqualTo("NO"));
            Assert.That(response.WeekForecast[0].ProbabilityPrecipitation, Is.EqualTo(0));
            Assert.That(response.WeekForecast[0].ProbabilitySunshine, Is.EqualTo(91));
        });

        Assert.Multiple(() =>
        {
            Assert.That(response.HourForecast, Has.Count.EqualTo(24));
            Assert.That(response.HourForecast[0].Hour, Is.EqualTo("31-08-2024 23:00"));
            Assert.That(response.HourForecast[0].Image, Is.EqualTo("nachtbewolkt"));
            Assert.That(response.HourForecast[0].Temperature, Is.EqualTo(19));
            Assert.That(response.HourForecast[0].WindSpeedBft, Is.EqualTo(4));
            Assert.That(response.HourForecast[0].WindSpeedKmh, Is.EqualTo(21));
            Assert.That(response.HourForecast[0].WindSpeedKn, Is.EqualTo(12));
            Assert.That(response.HourForecast[0].WindSpeedMs, Is.EqualTo(6));
            Assert.That(response.HourForecast[0].WindDirectionDegrees, Is.EqualTo(71));
            Assert.That(response.HourForecast[0].WindDirectionString, Is.EqualTo("NO"));
            Assert.That(response.HourForecast[0].Precipitation, Is.EqualTo(0));
            Assert.That(response.HourForecast[0].SolarIrradiance, Is.EqualTo(0));
        });

        Assert.Multiple(() =>
        {
            Assert.That(response.ApiInformationList, Has.Count.EqualTo(1));
            Assert.That(response.ApiInformation!.Source, Is.EqualTo("Bron: Weerdata KNMI/NOAA via Weerlive.nl"));
            Assert.That(response.ApiInformation.MaxRequests, Is.EqualTo(300));
            Assert.That(response.ApiInformation.RemainingRequests, Is.EqualTo(0));
        });
    }

    private static HttpClient MockClient(string query, string expectedResponse)
    {
        var handlerMock = new MockHttpMessageHandler();
        handlerMock
            .When(HttpMethod.Get, $"https://weerlive.nl/api/weerlive_api_v2.php{query}")
            .Respond("application/json", expectedResponse);
        return new HttpClient(handlerMock);
    }

    private const string DemoResponse = """
                                        {
                                            "liveweer": [
                                                {
                                                    "plaats": "Amsterdam",
                                                    "timestamp": 1725135483,
                                                    "time": "31-08-2024 22:18:03",
                                                    "temp": 18.9,
                                                    "gtemp": 17.3,
                                                    "samenv": "Licht bewolkt",
                                                    "lv": 71,
                                                    "windr": "ONO",
                                                    "windrgr": 62.1,
                                                    "windms": 6.33,
                                                    "windbft": 4,
                                                    "windknp": 12.3,
                                                    "windkmh": 22.8,
                                                    "luchtd": 1021.51,
                                                    "ldmmhg": 766,
                                                    "dauwp": 13.6,
                                                    "zicht": 30400,
                                                    "gr": 0,
                                                    "verw": "Vrij zonnig en vrijwel overal droog. Zondag in het zuiden tropisch warm",
                                                    "sup": "06:49",
                                                    "sunder": "20:32",
                                                    "image": "wolkennacht",
                                                    "alarm": 0,
                                                    "lkop": "Er zijn geen waarschuwingen",
                                                    "ltekst": " Er zijn momenteel geen waarschuwingen van kracht.",
                                                    "wrschklr": "groen",
                                                    "wrsch_g": "-",
                                                    "wrsch_gts": 0,
                                                    "wrsch_gc": "-"
                                                }
                                            ],
                                            "wk_verw": [
                                                {
                                                    "dag": "31-08-2024",
                                                    "image": "halfbewolkt",
                                                    "max_temp": 19,
                                                    "min_temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 67,
                                                    "windr": "NO",
                                                    "neersl_perc_dag": 0,
                                                    "zond_perc_dag": 91
                                                },
                                                {
                                                    "dag": "01-09-2024",
                                                    "image": "halfbewolkt",
                                                    "max_temp": 29,
                                                    "min_temp": 16,
                                                    "windbft": 3,
                                                    "windkmh": 14,
                                                    "windknp": 8,
                                                    "windms": 4,
                                                    "windrgr": 88,
                                                    "windr": "O",
                                                    "neersl_perc_dag": 60,
                                                    "zond_perc_dag": 66
                                                },
                                                {
                                                    "dag": "02-09-2024",
                                                    "image": "regen",
                                                    "max_temp": 28,
                                                    "min_temp": 19,
                                                    "windbft": 2,
                                                    "windkmh": 10,
                                                    "windknp": 6,
                                                    "windms": 3,
                                                    "windrgr": 154,
                                                    "windr": "ZO",
                                                    "neersl_perc_dag": 90,
                                                    "zond_perc_dag": 84
                                                },
                                                {
                                                    "dag": "03-09-2024",
                                                    "image": "halfbewolkt",
                                                    "max_temp": 24,
                                                    "min_temp": 17,
                                                    "windbft": 2,
                                                    "windkmh": 10,
                                                    "windknp": 6,
                                                    "windms": 3,
                                                    "windrgr": 162,
                                                    "windr": "ZO",
                                                    "neersl_perc_dag": 10,
                                                    "zond_perc_dag": 58
                                                },
                                                {
                                                    "dag": "04-09-2024",
                                                    "image": "halfbewolkt",
                                                    "max_temp": 20,
                                                    "min_temp": 17,
                                                    "windbft": 2,
                                                    "windkmh": 10,
                                                    "windknp": 6,
                                                    "windms": 3,
                                                    "windrgr": 129,
                                                    "windr": "ZO",
                                                    "neersl_perc_dag": 10,
                                                    "zond_perc_dag": 45
                                                }
                                            ],
                                            "uur_verw": [
                                                {
                                                    "uur": "31-08-2024 23:00",
                                                    "timestamp": 1725138000,
                                                    "image": "nachtbewolkt",
                                                    "temp": 19,
                                                    "windbft": 4,
                                                    "windkmh": 21,
                                                    "windknp": 12,
                                                    "windms": 6,
                                                    "windrgr": 71,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 00:00",
                                                    "timestamp": 1725141600,
                                                    "image": "nachtbewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 70,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 01:00",
                                                    "timestamp": 1725145200,
                                                    "image": "nachtbewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 69,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 02:00",
                                                    "timestamp": 1725148800,
                                                    "image": "nachtbewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 14,
                                                    "windknp": 8,
                                                    "windms": 4,
                                                    "windrgr": 73,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 03:00",
                                                    "timestamp": 1725152400,
                                                    "image": "nachtbewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 14,
                                                    "windknp": 8,
                                                    "windms": 4,
                                                    "windrgr": 66,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 04:00",
                                                    "timestamp": 1725156000,
                                                    "image": "wolkennacht",
                                                    "temp": 19,
                                                    "windbft": 4,
                                                    "windkmh": 21,
                                                    "windknp": 12,
                                                    "windms": 6,
                                                    "windrgr": 61,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 2848
                                                },
                                                {
                                                    "uur": "01-09-2024 05:00",
                                                    "timestamp": 1725159600,
                                                    "image": "wolkennacht",
                                                    "temp": 19,
                                                    "windbft": 4,
                                                    "windkmh": 21,
                                                    "windknp": 12,
                                                    "windms": 6,
                                                    "windrgr": 66,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 06:00",
                                                    "timestamp": 1725163200,
                                                    "image": "wolkennacht",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 70,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 07:00",
                                                    "timestamp": 1725166800,
                                                    "image": "bewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 64,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 08:00",
                                                    "timestamp": 1725170400,
                                                    "image": "bewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 72,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 09:00",
                                                    "timestamp": 1725174000,
                                                    "image": "helderenacht",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 78,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 10:00",
                                                    "timestamp": 1725177600,
                                                    "image": "bewolkt",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 14,
                                                    "windknp": 8,
                                                    "windms": 4,
                                                    "windrgr": 76,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 11:00",
                                                    "timestamp": 1725181200,
                                                    "image": "halfbewolkt",
                                                    "temp": 18,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 63,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 0
                                                },
                                                {
                                                    "uur": "01-09-2024 12:00",
                                                    "timestamp": 1725184800,
                                                    "image": "zonnig",
                                                    "temp": 18,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 77,
                                                    "windr": "NO",
                                                    "neersl": 0,
                                                    "gr": 50
                                                },
                                                {
                                                    "uur": "01-09-2024 13:00",
                                                    "timestamp": 1725188400,
                                                    "image": "zonnig",
                                                    "temp": 18,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 83,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 175
                                                },
                                                {
                                                    "uur": "01-09-2024 14:00",
                                                    "timestamp": 1725192000,
                                                    "image": "zonnig",
                                                    "temp": 19,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 97,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 321
                                                },
                                                {
                                                    "uur": "01-09-2024 15:00",
                                                    "timestamp": 1725195600,
                                                    "image": "zonnig",
                                                    "temp": 21,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 92,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 460
                                                },
                                                {
                                                    "uur": "01-09-2024 16:00",
                                                    "timestamp": 1725199200,
                                                    "image": "zonnig",
                                                    "temp": 23,
                                                    "windbft": 3,
                                                    "windkmh": 14,
                                                    "windknp": 8,
                                                    "windms": 4,
                                                    "windrgr": 103,
                                                    "windr": "ZO",
                                                    "neersl": 0,
                                                    "gr": 573
                                                },
                                                {
                                                    "uur": "01-09-2024 17:00",
                                                    "timestamp": 1725202800,
                                                    "image": "halfbewolkt",
                                                    "temp": 25,
                                                    "windbft": 3,
                                                    "windkmh": 14,
                                                    "windknp": 8,
                                                    "windms": 4,
                                                    "windrgr": 89,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 643
                                                },
                                                {
                                                    "uur": "01-09-2024 18:00",
                                                    "timestamp": 1725206400,
                                                    "image": "zonnig",
                                                    "temp": 27,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 89,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 679
                                                },
                                                {
                                                    "uur": "01-09-2024 19:00",
                                                    "timestamp": 1725210000,
                                                    "image": "zonnig",
                                                    "temp": 28,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 92,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 662
                                                },
                                                {
                                                    "uur": "01-09-2024 20:00",
                                                    "timestamp": 1725213600,
                                                    "image": "zonnig",
                                                    "temp": 29,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 97,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 604
                                                },
                                                {
                                                    "uur": "01-09-2024 21:00",
                                                    "timestamp": 1725217200,
                                                    "image": "nachtbewolkt",
                                                    "temp": 30,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 100,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 501
                                                },
                                                {
                                                    "uur": "01-09-2024 22:00",
                                                    "timestamp": 1725220800,
                                                    "image": "nachtbewolkt",
                                                    "temp": 30,
                                                    "windbft": 3,
                                                    "windkmh": 18,
                                                    "windknp": 10,
                                                    "windms": 5,
                                                    "windrgr": 97,
                                                    "windr": "O",
                                                    "neersl": 0,
                                                    "gr": 374
                                                }
                                            ],
                                            "api": [
                                                {
                                                    "bron": "Bron: Weerdata KNMI/NOAA via Weerlive.nl",
                                                    "max_verz": 300,
                                                    "rest_verz": 0
                                                }
                                            ]
                                        }
                                        """;
}