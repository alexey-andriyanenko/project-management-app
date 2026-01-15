using Facade.IdentityManagement.Contracts.Results;
using Facade.IdentityManagement.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facade.IdentityManagement.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/v1/auth")]
public class AuthManagementController(IAuthManagementService authManagementService)
{
    [HttpPost("login")]
    public async Task<LoginResult> LoginAsync([FromBody] Contracts.Parameters.Auth.LoginParameters parameters)
    {
        return await authManagementService.LoginAsync(parameters);
    }

    [HttpPost("register")]
    public async Task<RegisterResult> RegisterAsync([FromBody] Contracts.Parameters.Auth.RegisterParameters parameters)
    {
        return await authManagementService.RegisterAsync(parameters);
    }

    [HttpPost("refresh-token")]
    public async Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(
        [FromBody] Contracts.Parameters.Auth.RefreshAccessTokenParameters parameters)
    {
        return await authManagementService.RefreshAccessTokenAsync(parameters);
    }
}