namespace Facade.IdentityManagement.Services.Mappings.Parameters.User;

public static class UserParametersMappings
{
    public static Identity.Contracts.Parameters.User.CreateUserParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.User.CreateUserParameters parameters)
    {
        return new Identity.Contracts.Parameters.User.CreateUserParameters
        {
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            UserName = parameters.UserName,
            Password = parameters.Password,
        };
    }
    
    public static Identity.Contracts.Parameters.User.GetManyUsersByIdsParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.User.GetManyUsersByIdsParameters parameters)
    {
        return new Identity.Contracts.Parameters.User.GetManyUsersByIdsParameters()
        {
            Ids = parameters.Ids
        };
    }
    
    public static Identity.Contracts.Parameters.User.GetUserByIdParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.User.GetUserByIdParameters parameters)
    {
        return new Identity.Contracts.Parameters.User.GetUserByIdParameters()
        {
            Id = parameters.Id
        };
    }
    
    public static Identity.Contracts.Parameters.User.UpdateUserParameters ToCoreParameters(this Facade.IdentityManagement.Contracts.Parameters.User.UpdateUserParameters parameters)
    {
        return new Identity.Contracts.Parameters.User.UpdateUserParameters()
        {
            Id = parameters.Id,
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            UserName = parameters.UserName,
        };
    }
}