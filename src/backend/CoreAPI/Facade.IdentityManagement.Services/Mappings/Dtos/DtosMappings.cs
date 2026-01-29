namespace Facade.IdentityManagement.Services.Mappings.Dtos;

public static class DtosMappings
{
    public static Facade.IdentityManagement.Contracts.Dtos.UserDto ToFacadeDto(this Identity.Contracts.Dtos.UserDto dto)
    {
        return new Facade.IdentityManagement.Contracts.Dtos.UserDto
        {
            Id = dto.Id,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
        };
    }
    
    public static Facade.IdentityManagement.Contracts.Dtos.UserInvitationDto ToFacadeDto(this Identity.Contracts.Dtos.UserInvitationDto dto)
    {
        return new Facade.IdentityManagement.Contracts.Dtos.UserInvitationDto
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TenantMemberRole = dto.TenantMemberRole,
            InvitationLink = dto.InvitationLink,
            ExpiresAt = dto.ExpiresAt,
            CreatedAt = dto.CreatedAt,
            AcceptedAt = dto.AcceptedAt,
        };
    }
}