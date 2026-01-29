using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Services;
using Facade.IdentityManagement.Services.Mappings.Dtos;
using Facade.IdentityManagement.Services.Mappings.Parameters.User;

namespace Facade.IdentityManagement.Services;

public class UserManagementService(Identity.Client.Contracts.IIdentityClient identityClient) : IUserManagementService
{
    public async Task<UserDto> GetAsync(Facade.IdentityManagement.Contracts.Parameters.User.GetUserByIdParameters parameters)
    {
        var result = await identityClient.UserResource.GetAsync(parameters.ToCoreParameters());

        return result.ToFacadeDto();
    }
    
    public async Task<Contracts.Results.GetManyUsersByIdResult> GetManyAsync(Facade.IdentityManagement.Contracts.Parameters.User.GetManyUsersByIdsParameters parameters)
    {
        var result = await identityClient.UserResource.GetManyAsync(parameters.ToCoreParameters());

        return new Contracts.Results.GetManyUsersByIdResult
        {
            Users = result.Users.Select(u => u.ToFacadeDto()).ToList()
        };
    }
}