using Facade.TagManagement.Contracts.Dtos;
using Facade.TagManagement.Contracts.Parameters;
using Facade.TagManagement.Contracts.Results;

namespace Facade.TagManagement.Contracts.Services;

public interface ITagManagementService
{
    public Task<GetManyTagsByTenantIdResult> GetManyAsync(GetManyTagsByTenantIdParameters parameters);

    public Task<GetManyTagsByIdsResult> GetManyAsync(GetManyTagsByIdsParameters parameters);
    
    public Task<TagDto> CreateAsync(CreateTagParameters parameters);
    
    public Task<TagDto> UpdateAsync(UpdateTagParameters parameters);
    
    public Task DeleteAsync(DeleteTagParameters parameters);
}