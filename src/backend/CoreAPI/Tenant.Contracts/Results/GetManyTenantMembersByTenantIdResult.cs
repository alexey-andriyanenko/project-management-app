using Tenant.Contracts.Dtos;

namespace Tenant.Contracts.Results;

public class GetManyTenantMembersByTenantIdResult
{
    public IReadOnlyList<TenantMemberDto> TenantMembers { get; set; } = [];
}