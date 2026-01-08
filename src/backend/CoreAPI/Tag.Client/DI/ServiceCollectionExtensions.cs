using Microsoft.Extensions.DependencyInjection;
using Tag.Client.Contracts;
using Tag.Client.Contracts.Resources;
using Tag.Client.Resource;

namespace Tag.Client.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagClient(this IServiceCollection services)
    {
        services.AddScoped<ITagResource, TagResource>();
        services.AddScoped<ITagClient, TagClient>();
        
        return services;
    }
}