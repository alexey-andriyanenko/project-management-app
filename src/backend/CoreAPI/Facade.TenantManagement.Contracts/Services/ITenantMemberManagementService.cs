using Facade.TenantManagement.Contracts.Dtos;
using Facade.TenantManagement.Contracts.Parameters;
using Facade.TenantManagement.Contracts.Results;

namespace Facade.TenantManagement.Contracts.Services;

public interface ITenantMemberManagementService
{
    public Task<GetManyTenantMembersByTenantIdResult> GetManyAsync(GetManyTenantMembersByTenantIdParameters parameters);
    
    public Task<TenantMemberDto> CreateAsync(CreateTenantMemberParameters parameters);
    
    public Task<TenantMemberDto> RetryTenantMemberCreationFromInvitationAsync(RetryTenantMemberCreationFromInvitationParameters parameters);
    
    public Task<TenantMemberDto> UpdateAsync(UpdateTenantMemberParameters parameters);
    
    public Task DeleteAsync(DeleteTenantMemberParameters parameters);
}