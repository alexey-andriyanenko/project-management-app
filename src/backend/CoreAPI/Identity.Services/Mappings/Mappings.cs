using Identity.Contracts.Dtos;
using Identity.DataAccess.Entities;

namespace Identity.Services.Mappings;

public static class Mappings
{
    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            UserName = entity.UserName!,
        };
    }
    
    public static UserInvitationDto ToDto(this UserInvitationEntity entity, string invitationLink)
    {
        return new UserInvitationDto()
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            TenantMemberRole = entity.TenantMemberRole.ToString(),
            InvitationLink = invitationLink,
            ExpiresAt = entity.ExpiresAt,
            CreatedAt = entity.CreatedAt,
            AcceptedAt = entity.AcceptedAt,
        };
    }
}
