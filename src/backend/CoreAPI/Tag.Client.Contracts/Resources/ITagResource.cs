using Tag.Contracts.Dtos;
using Tag.Contracts.Parameters;
using Tag.Contracts.Result;

namespace Tag.Client.Contracts.Resources;

public interface ITagResource
{
    public Task<GetManyTagsByTenantIdResult> GetManyAsync(GetManyTagsByTenantIdParameters parameters);
    
    public Task<GetManyTagsByIdsResult> GetManyAsync(GetManyTagsByIdsParameters parameters);
    
    public Task<TagDto> CreateAsync(CreateTagParameters parameters);
    
    public Task<TagDto> UpdateAsync(UpdateTagParameters parameters);
    
    public Task DeleteAsync(DeleteTagParameters parameters);
}