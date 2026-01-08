using Identity.Contracts.Parameters.Auth;
using Identity.Contracts.Results;

namespace Identity.Contracts.Services;

public interface IAuthService
{
    public Task<LoginResult> LoginAsync(LoginParameters parameters);
    
    public Task<RegisterResult> RegisterAsync(RegisterParameters parameters);
    
    public Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(RefreshAccessTokenParameters parameters);
}