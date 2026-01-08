using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Services;
using Facade.IdentityManagement.Services.Mappings.Dtos;

namespace Facade.IdentityManagement.Services;

public class UserManagementService(Identity.Client.Contracts.IIdentityClient identityClient) : IUserManagementService
{
    public async Task<UserDto> GetAsync(Facade.IdentityManagement.Contracts.Parameters.User.GetUserByIdParameters parameters)
    {
        var result = await identityClient.UserResource.GetAsync(new Identity.Contracts.Parameters.User.GetUserByIdParameters
        {
            Id = parameters.Id
        });

        return result.ToFacadeDto();
    }
    
    public async Task<Contracts.Results.GetManyUsersByIdResult> GetManyAsync(Facade.IdentityManagement.Contracts.Parameters.User.GetManyUsersByIdsParameters parameters)
    {
        var result = await identityClient.UserResource.GetManyAsync(new Identity.Contracts.Parameters.User.GetManyUsersByIdsParameters
        {
            Ids = parameters.Ids
        });

        return new Contracts.Results.GetManyUsersByIdResult
        {
            Users = result.Users.Select(u => u.ToFacadeDto()).ToList()
        };
    }
    
    public async Task<UserDto> CreateAsync(Facade.IdentityManagement.Contracts.Parameters.User.CreateUserParameters parameters)
    {
        var result = await identityClient.UserResource.CreateAsync(new Identity.Contracts.Parameters.User.CreateUserParameters
        {
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            UserName = parameters.UserName,
            Password = parameters.Password
        });

        return result.ToFacadeDto();
    }
    
    public async Task<UserDto> UpdateAsync(Facade.IdentityManagement.Contracts.Parameters.User.UpdateUserParameters parameters)
    {
        var result = await identityClient.UserResource.UpdateAsync(new Identity.Contracts.Parameters.User.UpdateUserParameters
        {
            Id = parameters.Id,
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            UserName = parameters.UserName
        });

        return result.ToFacadeDto();
    }
}