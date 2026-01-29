using Identity.Contracts.Dtos;
using Identity.Contracts.Parameters.UserInvite;
using Identity.Contracts.Results;

namespace Identity.Contracts.Services;

public interface IUserInviteService
{
    public Task<GetManyUserInvitationsResult> GetManyAsync(GetManyUserInvitationsParameters parameters);
    
    public Task<UserInvitationDto> GetAsync(GetUserInvitationParameters parameters);
    
    public Task<UserInvitationDto> CreateAsync(CreateUserInvitationParameters parameters);
    
    public Task AcceptAsync(AcceptUserInvitationParameters parameters);
    
    public Task ResendAsync(ResendUserInvitationParameters parameters);
    
    public Task<ValidateUserInvitationResult> ValidateAsync(ValidateUserInvitationParameters parameters);
    
    public Task DeleteAsync(DeleteUserInvitationParameters parameters);
}
