using System.Net.Http.Json;
using System.Web;
using Microsoft.Extensions.Options;
using WeerLive.Lib.Models;

namespace WeerLive.Lib.Client;

public class WeerLiveClient(HttpClient client, IOptions<WeerLiveOptions> options)
    : IWeerLiveClient
{
    public async Task<WeerLiveResponse> GetAsync(string location, string? apiKey = null,
        CancellationToken token = default)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["key"] = apiKey ?? options.Value.ApiKey;
        query["locatie"] = location;

        return await GetAsync(query.ToString()!, token);
    }

    public WeerLiveResponse Get(string location, string? apiKey = null, CancellationToken token = default)
    {
        return GetAsync(location, apiKey, token).Result;
    }

    public async Task<WeerLiveResponse> GetAsync(decimal latitude, decimal longitude, string? apiKey = null,
        CancellationToken token = default)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["key"] = apiKey ?? options.Value.ApiKey;
        query["locatie"] = $"{latitude},{longitude}";

        return await GetAsync(query.ToString()!, token);
    }

    public WeerLiveResponse Get(decimal latitude, decimal longitude, string? apiKey = null,
        CancellationToken token = default)
    {
        return GetAsync(latitude, longitude, apiKey, token).Result;
    }

    private async Task<WeerLiveResponse> GetAsync(string query, CancellationToken token)
    {
        var response = await client.GetAsync($"{options.Value.BaseUrl}?{query}", token);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadFromJsonAsync<WeerLiveResponse>(token);
        if (responseContent is null)
            throw new WeerLiveException("Failed to retrieve weather data.");

        if (!string.IsNullOrEmpty(responseContent.LiveWeather?.Error))
            throw new WeerLiveException(responseContent.LiveWeather.Error);

        return responseContent;
    }
}