using Microsoft.Extensions.DependencyInjection;
using Tag.Contracts.Services;

namespace Tag.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagServices(this IServiceCollection services)
    {
        services.AddScoped<ITagService, TagService>();
        
        return services;
    }
}