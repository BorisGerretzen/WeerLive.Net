using System.Text.Json.Serialization;

namespace WeerLive.Lib.Models;

public class WeerLiveApiInformation(string source, int maxRequests, int remainingRequests)
{
    /// <summary>
    ///     Source of the data.
    /// </summary>
    [JsonPropertyName("bron")]
    public string Source { get; set; } = source;

    /// <summary>
    ///     Maximum number of requests per day.
    /// </summary>
    [JsonPropertyName("max_verz")]
    public int MaxRequests { get; set; } = maxRequests;

    /// <summary>
    ///     Remaining number of requests for the day.
    /// </summary>
    [JsonPropertyName("rest_verz")]
    public int RemainingRequests { get; set; } = remainingRequests;
}