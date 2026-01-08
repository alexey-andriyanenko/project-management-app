using Identity.Client.Contracts;
using Identity.Client.Contracts.Resources;
using Identity.Client.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Client.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityClient(this IServiceCollection services)
    {
        services.AddTransient<IAuthResource, AuthResource>();
        services.AddTransient<IUserResource, UserResource>();
        services.AddHttpClient<IIdentityClient, IdentityClient>();
        
        return services;
    }
}