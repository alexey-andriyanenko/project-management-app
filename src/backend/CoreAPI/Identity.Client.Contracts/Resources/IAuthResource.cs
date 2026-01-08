using Identity.Contracts.Parameters.Auth;
using Identity.Contracts.Results;

namespace Identity.Client.Contracts.Resources;

public interface IAuthResource
{
    public Task<LoginResult> LoginAsync(LoginParameters parameters);
    
    public Task<RegisterResult> RegisterAsync(RegisterParameters parameters);
    
    public Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(RefreshAccessTokenParameters parameters);
}