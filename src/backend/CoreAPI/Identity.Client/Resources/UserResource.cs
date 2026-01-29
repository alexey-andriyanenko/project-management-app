using Identity.Client.Contracts.Resources;
using Identity.Contracts.Dtos;
using Identity.Contracts.Parameters.User;
using Identity.Contracts.Results;
using Identity.Contracts.Services;

namespace Identity.Client.Resources;

public class UserResource(IUserService userService) : IUserResource
{
    public Task<UserDto> GetAsync(GetUserByIdParameters parameters)
        => userService.GetAsync(parameters);
    
    public Task<UserDto?> GetByEmailAsync(GetUserByEmailParameters parameters)
        => userService.GetByEmailAsync(parameters);
    
    public Task<GetManyUsersByIdResults> GetManyAsync(GetManyUsersByIdsParameters parameters)
        => userService.GetManyAsync(parameters);
    
    public Task<UserDto> CreateAsync(CreateUserParameters parameters)
        => userService.CreateAsync(parameters);
    
    public Task<UserDto> UpdateAsync(UpdateUserParameters parameters)
        => userService.UpdateAsync(parameters);
}