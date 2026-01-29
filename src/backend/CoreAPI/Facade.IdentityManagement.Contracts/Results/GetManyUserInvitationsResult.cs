using Facade.IdentityManagement.Contracts.Dtos;

namespace Facade.IdentityManagement.Contracts.Results;

public class GetManyUserInvitationsResult
{
    public IReadOnlyList<UserInvitationDto> Invitations { get; set; } = [];
}
