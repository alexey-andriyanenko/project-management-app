using Tag.Client.Contracts.Resources;
using Tag.Contracts.Dtos;
using Tag.Contracts.Parameters;
using Tag.Contracts.Result;
using Tag.Contracts.Services;

namespace Tag.Client.Resource;

public class TagResource(ITagService tagService) : ITagResource
{
    public Task<GetManyTagsByTenantIdResult> GetManyAsync(GetManyTagsByTenantIdParameters parameters)
        => tagService.GetManyAsync(parameters);
    
    public Task<GetManyTagsByIdsResult> GetManyAsync(GetManyTagsByIdsParameters parameters)
        => tagService.GetManyAsync(parameters);
    
    public Task<TagDto> CreateAsync(CreateTagParameters parameters)
        => tagService.CreateAsync(parameters);
    
    public Task<TagDto> UpdateAsync(UpdateTagParameters parameters)
        => tagService.UpdateAsync(parameters);
    
    public Task DeleteAsync(DeleteTagParameters parameters)
        => tagService.DeleteAsync(parameters);
}