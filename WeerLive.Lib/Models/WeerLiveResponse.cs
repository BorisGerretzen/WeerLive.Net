using System.Text.Json.Serialization;

namespace WeerLive.Lib.Models;

public class WeerLiveResponse(
    List<WeerLiveLiveWeather> liveWeatherList,
    List<WeerLiveWeekForecast> weekForecast,
    List<WeerLiveHourForecast> hourForecast,
    List<WeerLiveApiInformation> apiInformationList)
{
    /// <summary>
    ///     Live weather information.
    /// </summary>
    [JsonPropertyName("liveweer")]
    public List<WeerLiveLiveWeather> LiveWeatherList { get; } = liveWeatherList;

    /// <summary>
    ///     Week forecast information.
    /// </summary>
    [JsonPropertyName("wk_verw")]
    public List<WeerLiveWeekForecast> WeekForecast { get; } = weekForecast;

    /// <summary>
    ///     Hour forecast information.
    /// </summary>
    [JsonPropertyName("uur_verw")]
    public List<WeerLiveHourForecast> HourForecast { get; } = hourForecast;

    /// <summary>
    ///     API information.
    /// </summary>
    [JsonPropertyName("api")]
    public List<WeerLiveApiInformation> ApiInformationList { get; } = apiInformationList;

    public WeerLiveLiveWeather? LiveWeather => LiveWeatherList.FirstOrDefault();
    public WeerLiveApiInformation? ApiInformation => ApiInformationList.FirstOrDefault();
}