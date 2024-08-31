using System.Net.Http.Json;
using System.Web;
using Microsoft.Extensions.Options;
using WeerLive.Lib.Models;

namespace WeerLive.Lib.Client;

public class WeerLiveClient(HttpClient client, IOptions<WeerLiveOptions> options)
    : IWeerLiveClient
{
    private const string BaseUrl = "https://weerlive.nl/api/weerlive_api_v2.php";

    public async Task<WeerLiveResponse?> GetAsync(string location, string? apiKey = null,
        CancellationToken token = default)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["key"] = apiKey ?? options.Value.ApiKey;
        query["locatie"] = location;

        var response = await client.GetAsync($"{BaseUrl}?{query}", token);
        response.EnsureSuccessStatusCode();
        var str = await response.Content.ReadAsStringAsync(token);
        return await response.Content.ReadFromJsonAsync<WeerLiveResponse>(token);
    }

    public WeerLiveResponse? Get(string location, string? apiKey = null, CancellationToken token = default)
    {
        return GetAsync(location, apiKey, token).Result;
    }

    public async Task<WeerLiveResponse?> GetAsync(decimal latitude, decimal longitude, string? apiKey = null,
        CancellationToken token = default)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["key"] = apiKey ?? options.Value.ApiKey;
        query["locatie"] = $"{latitude},{longitude}";

        var response = await client.GetAsync($"{BaseUrl}?{query}", token);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<WeerLiveResponse>(token);
    }

    public WeerLiveResponse? Get(decimal latitude, decimal longitude, string? apiKey = null,
        CancellationToken token = default)
    {
        return GetAsync(latitude, longitude, apiKey, token).Result;
    }
}