using Identity.Client.DI;
using Identity.DataAccess.DI;
using Identity.Services.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityDataAccess(configuration)
            .AddIdentityServices()
            .AddIdentityClient();

        return services;
    }
}