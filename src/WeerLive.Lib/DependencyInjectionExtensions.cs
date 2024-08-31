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
    public static IServiceCollection AddWeerLive(this IServiceCollection services,
        Action<WeerLiveOptions>? configure = null)
    {
        if (configure is not null)
            services.Configure(configure);

        services.AddHttpClient<IWeerLiveClient, WeerLiveClient>();
        return services;
    }
}