using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tag.DataAccess.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTagDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TagDbContext>((sp, options) =>
        {
            var connectionString = configuration.GetConnectionString("TagManagementDb");
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(TagDbContext).Assembly.FullName);
            });
        });

        
        return services;
    }
}