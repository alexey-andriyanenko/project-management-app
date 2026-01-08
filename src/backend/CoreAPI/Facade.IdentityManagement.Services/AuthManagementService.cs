using Facade.IdentityManagement.Contracts.Results;
using Facade.IdentityManagement.Contracts.Services;
using Facade.IdentityManagement.Services.Mappings.Results;

namespace Facade.IdentityManagement.Services;

public class AuthManagementService(Identity.Client.Contracts.IIdentityClient identityClient) : IAuthManagementService
{
    public async Task<LoginResult> LoginAsync(Contracts.Parameters.Auth.LoginParameters parameters)
    {
        try
        {
            var result = await identityClient.AuthResource.LoginAsync(new Identity.Contracts.Parameters.Auth.LoginParameters
            {
                Email = parameters.Email,
                Password = parameters.Password
            });

            return result.ToFacadeResult();
        }
        catch(Identity.Contracts.Exceptions.InvalidCredentialsException ex)
        {
            throw new Facade.IdentityManagement.Contracts.Exceptions.InvalidCredentialsExceptions();
        }
    }
    
    public async Task<RegisterResult> RegisterAsync(Contracts.Parameters.Auth.RegisterParameters parameters)
    {
        var result = await identityClient.AuthResource.RegisterAsync(new Identity.Contracts.Parameters.Auth.RegisterParameters
        {
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            UserName = parameters.UserName,
            Email = parameters.Email,
            Password = parameters.Password
        });

        return result.ToFacadeResult();
    }
    
    public async Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(Contracts.Parameters.Auth.RefreshAccessTokenParameters parameters)
    {
        try
        {
            var result = await identityClient.AuthResource.RefreshAccessTokenAsync(new Identity.Contracts.Parameters.Auth.RefreshAccessTokenParameters
            {
                RefreshToken = parameters.RefreshToken
            });

            return result.ToFacadeResult();
        } 
        catch (Identity.Contracts.Exceptions.RefreshTokenNotFoundException ex)
        {
            throw new Facade.IdentityManagement.Contracts.Exceptions.RefreshTokenNotFoundException();
        }
        catch(Identity.Contracts.Exceptions.RefreshTokenExpiredException ex)
        {
            throw new Facade.IdentityManagement.Contracts.Exceptions.RefreshTokenExpiredException();
        }
        catch(Identity.Contracts.Exceptions.RefreshTokenAlreadyRevokedException ex)
        {
            throw new Facade.IdentityManagement.Contracts.Exceptions.RefreshTokenAlreadyRevokedException();
        }
    }
}