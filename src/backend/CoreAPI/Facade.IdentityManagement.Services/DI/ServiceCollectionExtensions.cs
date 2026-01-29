using Facade.IdentityManagement.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Facade.IdentityManagement.Services.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityManagementFacadeServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthManagementService, AuthManagementService>();
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<IUserInvitationManagementService, UserInvitationManagementService>();
        
        return services;
    }
}