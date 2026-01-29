using Identity.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserInviteService, UserInvitationService>();

        return services;
    }
}