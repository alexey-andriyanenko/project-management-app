using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DataAccess.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityDataAccess(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>((sp, options) => {
            var connectionString = configuration["IdentityManagementService:DbConnection"];

            options.UseNpgsql(connectionString, npgsqlOptions => {
                npgsqlOptions.MigrationsAssembly(typeof(IdentityDbContext).Assembly.FullName);
            });
        });

        services.AddIdentity<UserEntity, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
