using Facade.IdentityManagement.Contracts.Parameters.Auth;
using Facade.IdentityManagement.Contracts.Results;

namespace Facade.IdentityManagement.Contracts.Services;

public interface IAuthManagementService
{
    public Task<LoginResult> LoginAsync(LoginParameters parameters);
    
    public Task<RegisterResult> RegisterAsync(RegisterParameters parameters);
    
    public Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(RefreshAccessTokenParameters parameters);
}