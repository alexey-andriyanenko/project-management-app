namespace Facade.TenantManagement.Services.Mappings;

public static class DtosParameters
{
    public static Facade.TenantManagement.Contracts.Dtos.TenantDto ToFacadeDto(this Tenant.Contracts.Dtos.TenantDto dto)
    {
        return new Facade.TenantManagement.Contracts.Dtos.TenantDto
        {
            Id = dto.Id,
            Name = dto.Name,
            Slug = dto.Slug
        };
    }
    
    public static Facade.TenantManagement.Contracts.Dtos.TenantMemberDto ToFacadeDto(this Tenant.Contracts.Dtos.TenantMemberDto tenant, Identity.Contracts.Dtos.UserDto user)
    {
        return new Facade.TenantManagement.Contracts.Dtos.TenantMemberDto
        {
            TenantId = tenant.TenantId,
            UserId = tenant.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Role = (Facade.TenantManagement.Contracts.Dtos.TenantMemberRole)tenant.Role
        };
    }
}