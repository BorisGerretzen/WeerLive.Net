﻿using System.Text.Json.Serialization;

namespace WeerLive.Lib.Models;

public class WeerLiveWeekForecast(
    string day,
    string image,
    int maxTemperature,
    int minTemperature,
    int windSpeedBft,
    int windSpeedKmh,
    int windSpeedKn,
    int windSpeedMs,
    int windDirectionDegrees,
    string windDirection,
    int probabilityPrecipitation,
    int probabilitySunshine
)
{
    /// <summary>
    ///     Date of the forecast.
    ///     <example>31-08-2024</example>
    /// </summary>
    [JsonPropertyName("dag")]
    public string Day { get; } = day;

    /// <summary>
    ///     Image name of the forecast.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; } = image;

    /// <summary>
    ///     Maximum temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("max_temp")]
    public int MaxTemperature { get; } = maxTemperature;

    /// <summary>
    ///     Minimum temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("min_temp")]
    public int MinTemperature { get; init; } = minTemperature;

    /// <summary>
    ///     Wind speed in Beaufort.
    /// </summary>
    [JsonPropertyName("windbft")]
    public int WindSpeedBft { get; } = windSpeedBft;

    /// <summary>
    ///     Wind speed in kilometers per hour.
    /// </summary>
    [JsonPropertyName("windkmh")]
    public int WindSpeedKmh { get; } = windSpeedKmh;

    /// <summary>
    ///     Wind speed in knots.
    /// </summary>
    [JsonPropertyName("windknp")]
    public int WindSpeedKn { get; } = windSpeedKn;

    /// <summary>
    ///     Wind speed in meters per second.
    /// </summary>
    [JsonPropertyName("windms")]
    public int WindSpeedMs { get; } = windSpeedMs;

    /// <summary>
    ///     Wind direction in degrees.
    /// </summary>
    [JsonPropertyName("windrgr")]
    public int WindDirectionDegrees { get; } = windDirectionDegrees;

    /// <summary>
    ///     Wind direction in compass direction (Dutch).
    /// </summary>
    [JsonPropertyName("windr")]
    public string WindDirection { get; } = windDirection;

    /// <summary>
    ///     Probability of precipitation in percent points.
    /// </summary>
    [JsonPropertyName("neersl_perc_dag")]
    public int ProbabilityPrecipitation { get; } = probabilityPrecipitation;

    /// <summary>
    ///     Probability of sun in percent points.
    /// </summary>
    [JsonPropertyName("zond_perc_dag")]
    public int ProbabilitySunshine { get; } = probabilitySunshine;
}