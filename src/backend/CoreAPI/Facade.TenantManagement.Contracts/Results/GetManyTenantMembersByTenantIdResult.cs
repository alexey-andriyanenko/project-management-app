using Facade.TenantManagement.Contracts.Dtos;

namespace Facade.TenantManagement.Contracts.Results;

public class GetManyTenantMembersByTenantIdResult
{
    public IReadOnlyList<TenantMemberDto> TenantMembers { get; set; } = [];
}