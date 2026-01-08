using Facade.TagManagement.Contracts.Dtos;
using Facade.TagManagement.Contracts.Exceptions;
using Facade.TagManagement.Contracts.Parameters;
using Facade.TagManagement.Contracts.Results;
using Facade.TagManagement.Contracts.Services;
using Facade.TagManagement.Services.Mappings;

namespace Facade.TagManagement.Services;

public class TagManagementService(Tag.Client.Contracts.ITagClient tagClient) : ITagManagementService
{
    public async Task<GetManyTagsByTenantIdResult> GetManyAsync(GetManyTagsByTenantIdParameters parameters)
    {
        var result = await tagClient.TagResource.GetManyAsync(parameters.ToCoreParameters());

        return new GetManyTagsByTenantIdResult
        {
            Tags = result.Tags.Select(t => t.ToFacadeDto()).ToList()
        };
    }
    
    public async Task<GetManyTagsByIdsResult> GetManyAsync(GetManyTagsByIdsParameters parameters)
    {
        try
        {
            var result = await tagClient.TagResource.GetManyAsync(parameters.ToCoreParameters());

            return new GetManyTagsByIdsResult
            {
                Tags = result.Tags.Select(t => t.ToFacadeDto()).ToList()
            };
        }
        catch (Tag.Contracts.Exceptions.TagNotFoundException)
        {
            throw new TagNotFoundException(parameters.TagIds.First());
        }
    }
    
    public async Task<TagDto> CreateAsync(CreateTagParameters parameters)
    {
        var result = await tagClient.TagResource.CreateAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }
    
    public async Task<TagDto> UpdateAsync(UpdateTagParameters parameters)
    {
        try
        {
            var result = await tagClient.TagResource.UpdateAsync(parameters.ToCoreParameters());
            return result.ToFacadeDto();
        }
        catch (Tag.Contracts.Exceptions.TagNotFoundException) 
        {
            throw new TagNotFoundException(parameters.Id);
        }
    }
    
    public async Task DeleteAsync(DeleteTagParameters parameters)
    {
        try 
        {
            await tagClient.TagResource.DeleteAsync(parameters.ToCoreParameters());
        }
        catch (Tag.Contracts.Exceptions.TagNotFoundException) 
        {
            throw new TagNotFoundException(parameters.Id);
        }
    }
}