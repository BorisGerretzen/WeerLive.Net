﻿using System.Text.Json.Serialization;

namespace WeerLive.Lib.Models;

public class WeerLiveLiveWeather(
    string place,
    long timestamp,
    string time,
    double temperature,
    double temperaturePerceived,
    string summary,
    int humidity,
    string windDirection,
    double windDirectionDegrees,
    double windSpeedMs,
    int windSpeedBft,
    double windSpeedKn,
    double windSpeedKmh,
    double airPressure,
    int airPressureMmHg,
    double dewPoint,
    int visibility,
    int solarIrradiance,
    string dayForecast,
    string sunrise,
    string sunset,
    string image,
    int weatherAlert,
    string weatherAlertHeader,
    string weatherAlertDescription,
    string warningColor,
    string weatherAlertStart,
    long weatherAlertTimestamp,
    string weatherAlertColorCode,
    string? error)
{
    /// <summary>
    ///     The location of the weather data.
    /// </summary>
    [JsonPropertyName("plaats")]
    public string Place { get; } = place;

    /// <summary>
    ///     The timestamp of the weather data in unix seconds.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; } = timestamp;

    /// <summary>
    ///     The date and time of the weather data in CET.
    ///     <example>31-08-2024 22:18:03</example>
    /// </summary>
    [JsonPropertyName("time")]
    public string Time { get; } = time;

    /// <summary>
    ///     The temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("temp")]
    public double Temperature { get; } = temperature;

    /// <summary>
    ///     The perceived temperature in degrees Celsius.
    /// </summary>
    [JsonPropertyName("gtemp")]
    public double TemperaturePerceived { get; } = temperaturePerceived;

    /// <summary>
    ///     The summary of the weather in Dutch.
    /// </summary>
    [JsonPropertyName("samenv")]
    public string Summary { get; } = summary;

    /// <summary>
    ///     Relative humidity in percent points.
    /// </summary>
    [JsonPropertyName("lv")]
    public int Humidity { get; } = humidity;

    /// <summary>
    ///     Wind direction in compass direction (Dutch).
    /// </summary>
    [JsonPropertyName("windr")]
    public string WindDirection { get; } = windDirection;

    /// <summary>
    ///     Wind direction in degrees.
    /// </summary>
    [JsonPropertyName("windrgr")]
    public double WindDirectionDegrees { get; } = windDirectionDegrees;

    /// <summary>
    ///     Wind speed in meters per second.
    /// </summary>
    [JsonPropertyName("windms")]
    public double WindSpeedMs { get; } = windSpeedMs;

    /// <summary>
    ///     Wind speed in Beaufort.
    /// </summary>
    [JsonPropertyName("windbft")]
    public int WindSpeedBft { get; } = windSpeedBft;

    /// <summary>
    ///     Wind speed in knots.
    /// </summary>
    [JsonPropertyName("windknp")]
    public double WindSpeedKn { get; } = windSpeedKn;

    /// <summary>
    ///     Wind speed in kilometers per hour.
    /// </summary>
    [JsonPropertyName("windkmh")]
    public double WindSpeedKmh { get; } = windSpeedKmh;

    /// <summary>
    ///     Air pressure in hPa.
    /// </summary>
    [JsonPropertyName("luchtd")]
    public double AirPressure { get; } = airPressure;

    /// <summary>
    ///     Air pressure in mmHg.
    /// </summary>
    [JsonPropertyName("ldmmhg")]
    public int AirPressureMmHg { get; } = airPressureMmHg;

    /// <summary>
    ///     Dew point in degrees Celsius.
    /// </summary>
    [JsonPropertyName("dauwp")]
    public double DewPoint { get; } = dewPoint;

    /// <summary>
    ///     Visibility in kilometers.
    /// </summary>
    [JsonPropertyName("zicht")]
    public int Visibility { get; } = visibility;

    /// <summary>
    ///     Solar irradiance in W/m^2.
    /// </summary>
    [JsonPropertyName("gr")]
    public int SolarIrradiance { get; } = solarIrradiance;

    /// <summary>
    ///     Short day forecast in Dutch.
    /// </summary>
    [JsonPropertyName("verw")]
    public string DayForecast { get; } = dayForecast;

    /// <summary>
    ///     The time of sunrise in (presumably) CET.
    /// </summary>
    /// <example>06:49</example>
    [JsonPropertyName("sup")]
    public string Sunrise { get; } = sunrise;

    /// <summary>
    ///     The time of sunset in (presumably) CET.
    /// </summary>
    /// <example>20:32</example>
    [JsonPropertyName("sunder")]
    public string Sunset { get; } = sunset;

    /// <summary>
    ///     Image name of the forecast.
    /// </summary>
    [JsonPropertyName("image")]
    public string Image { get; } = image;

    /// <summary>
    ///     Indicates if there is a weather alert.
    /// </summary>
    [JsonPropertyName("alarm")]
    public int WeatherAlert { get; } = weatherAlert;

    /// <summary>
    ///     Weather alert header.
    /// </summary>
    [JsonPropertyName("lkop")]
    public string WeatherAlertHeader { get; } = weatherAlertHeader;

    /// <summary>
    ///     Weather alert description.
    /// </summary>
    [JsonPropertyName("ltekst")]
    public string WeatherAlertDescription { get; } = weatherAlertDescription;

    /// <summary>
    ///     KNMI color code for the region.
    /// </summary>
    [JsonPropertyName("wrschklr")]
    public string WarningColor { get; } = warningColor;

    /// <summary>
    ///     Time the weather alert starts.
    /// </summary>
    [JsonPropertyName("wrsch_g")]
    public string WeatherAlertStart { get; } = weatherAlertStart;

    /// <summary>
    ///     Weather alert timestamp.
    /// </summary>
    [JsonPropertyName("wrsch_gts")]
    public long WeatherAlertTimestamp { get; } = weatherAlertTimestamp;

    /// <summary>
    ///     KNMI color code for the weather alert.
    /// </summary>
    [JsonPropertyName("wrsch_gc")]
    public string WeatherAlertColorCode { get; } = weatherAlertColorCode;

    /// <summary>
    ///     Error message if the request failed.
    /// </summary>
    [JsonPropertyName("fout")]
    public string? Error { get; } = error;
}