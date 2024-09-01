namespace WeerLive.Lib.Client;

public class WeerLiveOptions
{
    public string? ApiKey { get; set; }
    public string BaseUrl { get; set; } = "https://weerlive.nl/api/weerlive_api_v2.php";
}