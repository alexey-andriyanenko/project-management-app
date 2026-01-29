using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Parameters.User;
using Facade.IdentityManagement.Contracts.Results;

namespace Facade.IdentityManagement.Contracts.Services;

public interface IUserManagementService
{
    public Task<UserDto> GetAsync(GetUserByIdParameters parameters);
    
    public Task<GetManyUsersByIdResult> GetManyAsync(GetManyUsersByIdsParameters parameters);
}