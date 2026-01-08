using Identity.Client.Contracts.Resources;
using Identity.Contracts.Parameters.Auth;
using Identity.Contracts.Results;
using Identity.Contracts.Services;

namespace Identity.Client.Resources;

public class AuthResource(IAuthService authService) : IAuthResource
{
    public Task<LoginResult> LoginAsync(LoginParameters parameters)
        => authService.LoginAsync(parameters);
    
    public Task<RegisterResult> RegisterAsync(RegisterParameters parameters)
        => authService.RegisterAsync(parameters);
    
    public Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(RefreshAccessTokenParameters parameters)
        => authService.RefreshAccessTokenAsync(parameters);
}