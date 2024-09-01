using Microsoft.Extensions.DependencyInjection;
using WeerLive.Lib.Client;

namespace WeerLive.Lib;

public static class DependencyInjectionExtensions
{
    /// <summary>
    ///     Registers the WeerLive client in the service collection.
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configure">API key provider.</param>
    /// <param name="configureHttpClient">Delegate used to configure the http client.</param>
    public static IServiceCollection AddWeerLive(this IServiceCollection services,
        Action<WeerLiveOptions>? configure = null,
        Action<HttpClient>? configureHttpClient = null)
    {
        if (configure is not null)
            services.Configure(configure);

        configureHttpClient ??= _ => { };

        services.AddHttpClient<IWeerLiveClient, WeerLiveClient>(configureHttpClient);
        return services;
    }
}