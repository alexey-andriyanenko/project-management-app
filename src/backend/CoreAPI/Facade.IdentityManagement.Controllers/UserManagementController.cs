using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Parameters.User;
using Facade.IdentityManagement.Contracts.Results;
using Facade.IdentityManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.IdentityManagement.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserManagementController(IUserManagementService userManagementService)
{
    [HttpGet("{userId}")]
    public async Task<UserDto> GetAsync([FromRoute] Guid userId)
    {
        var parameters = new Contracts.Parameters.User.GetUserByIdParameters
        {
            Id = userId
        };
        return await userManagementService.GetAsync(parameters);
    }
    
    [HttpGet]
    public async Task<GetManyUsersByIdResult> GetManyAsync([FromQuery] GetManyUsersByIdsParameters parameters)
    {
        return await userManagementService.GetManyAsync(new GetManyUsersByIdsParameters()
        {
            Ids = parameters.Ids
        });
    }

    [HttpPost]
    public async Task<UserDto> CreateAsync([FromBody] CreateUserParameters parameters)
    {
        return await userManagementService.CreateAsync(parameters);
    }

    [HttpPut("{userId}")]
    public async Task<UserDto> UpdateAsync([FromRoute] Guid userId, [FromBody] UpdateUserParameters parameters)
    {
        parameters.Id = userId;
        return await userManagementService.UpdateAsync(parameters);
    }
}