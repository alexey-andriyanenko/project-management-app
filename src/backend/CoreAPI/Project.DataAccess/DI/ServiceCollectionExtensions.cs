using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.DataAccess.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("ProjectManagementDb");
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(ProjectDbContext).Assembly.FullName);
            });
        });
        
        return services;
    }
}