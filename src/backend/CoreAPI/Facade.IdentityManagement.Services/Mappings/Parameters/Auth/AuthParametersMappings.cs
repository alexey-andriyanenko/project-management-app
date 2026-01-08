namespace Facade.IdentityManagement.Services.Mappings.Parameters.Auth;

public static class AuthParametersMappings
{
    public static Identity.Contracts.Parameters.Auth.LoginParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.Auth.LoginParameters parameters)
    {
        return new Identity.Contracts.Parameters.Auth.LoginParameters
        {
            Email = parameters.Email,
            Password = parameters.Password,
        };
    }
    
    public static Identity.Contracts.Parameters.Auth.RefreshAccessTokenParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.Auth.RefreshAccessTokenParameters parameters)
    {
        return new Identity.Contracts.Parameters.Auth.RefreshAccessTokenParameters
        {
            RefreshToken = parameters.RefreshToken,
        };
    }
    
    public static Identity.Contracts.Parameters.Auth.RegisterParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.Auth.RegisterParameters parameters)
    {
        return new Identity.Contracts.Parameters.Auth.RegisterParameters
        {
            Email = parameters.Email,
            Password = parameters.Password,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            UserName = parameters.UserName,
        };
    }
}