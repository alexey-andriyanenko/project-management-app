using Identity.Contracts.Dtos;
using Identity.Contracts.Parameters.User;
using Identity.Contracts.Results;

namespace Identity.Client.Contracts.Resources;

public interface IUserResource
{
    public Task<UserDto> GetAsync(GetUserByIdParameters parameters);
    
    public Task<GetManyUsersByIdResults> GetManyAsync(GetManyUsersByIdsParameters parameters);
    
    public Task<UserDto> CreateAsync(CreateUserParameters parameters);
    
    public Task<UserDto> UpdateAsync(UpdateUserParameters parameters);
}