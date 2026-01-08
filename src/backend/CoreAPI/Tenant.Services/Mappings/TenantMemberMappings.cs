using Tenant.Contracts.Dtos;
using Tenant.DataAccess.Entities;

namespace Tenant.Services.Mappings;

public static class TenantMemberMappings
{
    public static TenantMemberDto ToDto(this TenantMemberEntity tenantMember)
    {
        return new TenantMemberDto
        {
            TenantId = tenantMember.TenantId,
            UserId = tenantMember.UserId,
            Role = (TenantMemberRole)tenantMember.Role
        };
    }
}