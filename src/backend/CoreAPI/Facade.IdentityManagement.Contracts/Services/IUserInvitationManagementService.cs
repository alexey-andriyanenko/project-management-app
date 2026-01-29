using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Parameters.UserInvitation;
using Facade.IdentityManagement.Contracts.Results;

namespace Facade.IdentityManagement.Contracts.Services;

public interface IUserInvitationManagementService
{
    public Task<GetManyUserInvitationsResult> GetManyAsync(GetManyUserInvitationsParameters parameters);
    
    public Task<UserInvitationDto> CreateAsync(CreateUserInvitationParameters parameters);
    
    public Task AcceptAsync(AcceptUserInvitationParameters parameters);
    
    public Task ResendAsync(ResendUserInvitationParameters parameters);
    
    public Task<ValidateUserInvitationResult> ValidateAsync(ValidateUserInvitationParameters parameters);
    
    public Task DeleteAsync(DeleteUserInvitationParameters parameters);
}
