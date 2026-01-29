using Identity.Contracts.Dtos;

namespace Identity.Contracts.Results;

public class GetManyUserInvitationsResult
{
    public IReadOnlyList<UserInvitationDto> Invitations { get; set; } = [];
}