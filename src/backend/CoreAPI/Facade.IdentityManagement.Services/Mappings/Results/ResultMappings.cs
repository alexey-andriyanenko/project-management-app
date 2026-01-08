namespace Facade.IdentityManagement.Services.Mappings.Results;

public static class ResultMappings
{
    public static Facade.IdentityManagement.Contracts.Results.LoginResult ToFacadeResult(this Identity.Contracts.Results.LoginResult source)
    {
        return new Facade.IdentityManagement.Contracts.Results.LoginResult
        {
            UserId = source.UserId,
            AccessToken = source.AccessToken,
            RefreshToken = source.RefreshToken
        };
    }
    
    public static Facade.IdentityManagement.Contracts.Results.RegisterResult ToFacadeResult(this Identity.Contracts.Results.RegisterResult source) 
    {
        return new Facade.IdentityManagement.Contracts.Results.RegisterResult
        {
            UserId = source.UserId,
            AccessToken = source.AccessToken,
            RefreshToken = source.RefreshToken
        };
    }
    
    public static Facade.IdentityManagement.Contracts.Results.RefreshAccessTokenResult ToFacadeResult(this Identity.Contracts.Results.RefreshAccessTokenResult source) 
    {
        return new Facade.IdentityManagement.Contracts.Results.RefreshAccessTokenResult
        {
            AccessToken = source.AccessToken,
            RefreshToken = source.RefreshToken
        };
    }
}