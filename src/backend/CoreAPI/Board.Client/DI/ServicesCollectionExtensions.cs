using Board.Client.Contracts;
using Board.Client.Contracts.Resources;
using Board.Client.Resources;
using Board.Services.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Board.Client.DI;

public static class ServicesCollectionExtensions
{
    public static IServiceCollection AddBoardClientServices(this IServiceCollection services)
    {
        services.AddScoped<IBoardResource, BoardResource>();
        services.AddScoped<IBoardColumnResource, BoardColumnResource>();
        services.AddScoped<IBoardTypeResource, BoardTypeResource>();
        services.AddScoped<ITaskResource, TaskResource>();
        services.AddScoped<IBoardClient, BoardClient>();
        
        return services;
    }
}
