using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Parameters.UserInvitation;
using Facade.IdentityManagement.Contracts.Results;
using Facade.IdentityManagement.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facade.IdentityManagement.Controllers;

[ApiController]
[Route("api/v1/user-invitations")]
public class UserInvitationManagementController(IUserInvitationManagementService userInvitationManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<GetManyUserInvitationsResult> GetManyAsync([FromQuery] GetManyUserInvitationsParameters parameters)
    {
        return await userInvitationManagementService.GetManyAsync(parameters);
    }

    [HttpPost]
    public async Task<UserInvitationDto> CreateAsync([FromBody] CreateUserInvitationParameters parameters)
    {
        return await userInvitationManagementService.CreateAsync(parameters);
    }

    [HttpPost("{invitationId}/resend")]
    public async Task<IActionResult> ResendAsync([FromRoute] Guid invitationId)
    {
        var parameters = new ResendUserInvitationParameters
        {
            InvitationId = invitationId
        };
        
        await userInvitationManagementService.ResendAsync(parameters);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("validate")]
    public async Task<ValidateUserInvitationResult> ValidateAsync([FromBody] ValidateUserInvitationParameters parameters)
    {
        return await userInvitationManagementService.ValidateAsync(parameters);
    }

    [AllowAnonymous]
    [HttpPost("accept")]
    public async Task<IActionResult> AcceptAsync([FromBody] AcceptUserInvitationParameters parameters)
    {
        await userInvitationManagementService.AcceptAsync(parameters);
        return NoContent();
    }

    [HttpDelete("{invitationId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid invitationId)
    {
        var parameters = new DeleteUserInvitationParameters
        {
            InvitationId = invitationId
        };
        
        await userInvitationManagementService.DeleteAsync(parameters);
        return NoContent();
    }
}
