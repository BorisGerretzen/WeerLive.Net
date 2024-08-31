using System.Text.Json.Serialization;

namespace WeerLive.Lib.Models;

public class WeerLiveHourForecast(
    string hour,
    long timestamp,
    string image,
    int temperature,
    int windSpeedBft,
    int windSpeedKmh,
    int windSpeedKn,
    int windSpeedMs,
    int windDirectionDegrees,
    string windDirectionString,
    int precipitation,
    int solarIrradiance
)
{
    /// <summary>
    ///     Date of the forecast.
    ///     <example>
    ///         31-08-2024 23:00
    ///     </example>
    /// </summary>
    [JsonPropertyName("uur")]
    public string Hour { get; } = hour;

    /// <summary>
    ///     Timestamp of the forecast in unix seconds.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; } = timestamp;

    /// <summary>
    ///     Image name of the forecast.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; } = image;

    /// <summary>
    ///     Temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("temp")]
    public int Temperature { get; } = temperature;

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
    public string WindDirectionString { get; } = windDirectionString;

    /// <summary>
    ///     Precipitation in millimeters.
    /// </summary>
    [JsonPropertyName("neersl")]
    public int Precipitation { get; } = precipitation;

    /// <summary>
    ///     Solar irradiance in watts per square meter.
    /// </summary>
    [JsonPropertyName("gr")]
    public int SolarIrradiance { get; } = solarIrradiance;
}