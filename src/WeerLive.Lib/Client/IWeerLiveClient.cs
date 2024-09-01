using WeerLive.Lib.Models;

namespace WeerLive.Lib.Client;

public interface IWeerLiveClient
{
    /// <summary>
    ///     Gets the weather forecast for the given location.
    /// </summary>
    /// <param name="location">Location to get forecast for.</param>
    /// <param name="apiKey">API key to use if no provider is registered.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Response from WeerLive.</returns>
    Task<WeerLiveResponse> GetAsync(string location, string? apiKey = null, CancellationToken token = default);

    /// <inheritdoc cref="GetAsync(string,string?,CancellationToken)" />
    WeerLiveResponse? Get(string location, string? apiKey = null, CancellationToken token = default);

    /// <summary>
    ///     <inheritdoc cref="GetAsync(string,string?,CancellationToken)" />
    /// </summary>
    /// <param name="latitude">Latitude of the location.</param>
    /// <param name="longitude">Longitude of the location.</param>
    /// <param name="apiKey">API key to use if no provider is registered.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Response from WeerLive.</returns>
    Task<WeerLiveResponse> GetAsync(decimal latitude, decimal longitude, string? apiKey = null,
        CancellationToken token = default);

    /// <inheritdoc cref="GetAsync(decimal,decimal,string?,CancellationToken)" />
    WeerLiveResponse? Get(decimal latitude, decimal longitude, string? apiKey = null,
        CancellationToken token = default);
}