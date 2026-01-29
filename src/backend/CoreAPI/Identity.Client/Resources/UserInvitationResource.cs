using Identity.Client.Contracts.Resources;
using Identity.Contracts.Dtos;
using Identity.Contracts.Parameters.UserInvite;
using Identity.Contracts.Results;
using Identity.Contracts.Services;

namespace Identity.Client.Resources;

public class UserInvitationResource(IUserInviteService userInviteService) : IUserInvitationResource
{
    public Task<GetManyUserInvitationsResult> GetManyAsync(GetManyUserInvitationsParameters parameters)
        => userInviteService.GetManyAsync(parameters);
    
    public Task<UserInvitationDto> GetAsync(GetUserInvitationParameters parameters)
        => userInviteService.GetAsync(parameters);
    
    public Task<UserInvitationDto> CreateAsync(CreateUserInvitationParameters parameters)
        => userInviteService.CreateAsync(parameters);
    
    public Task AcceptAsync(AcceptUserInvitationParameters parameters)
        => userInviteService.AcceptAsync(parameters);
    
    public Task ResendAsync(ResendUserInvitationParameters parameters)
        => userInviteService.ResendAsync(parameters);
    
    public Task<ValidateUserInvitationResult> ValidateAsync(ValidateUserInvitationParameters parameters)
        => userInviteService.ValidateAsync(parameters);
    
    public Task DeleteAsync(DeleteUserInvitationParameters parameters)
        => userInviteService.DeleteAsync(parameters);
}
