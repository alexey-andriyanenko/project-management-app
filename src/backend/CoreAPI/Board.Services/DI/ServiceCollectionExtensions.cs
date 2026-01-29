using Board.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Board.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBoardServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IBoardColumnService, BoardColumnService>();
        services.AddScoped<IBoardTypeService, BoardTypeService>();
        
        return services;
    }
}
