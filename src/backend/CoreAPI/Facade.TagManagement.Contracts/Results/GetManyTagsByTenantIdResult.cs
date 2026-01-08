using Facade.TagManagement.Contracts.Dtos;

namespace Facade.TagManagement.Contracts.Results;

public class GetManyTagsByTenantIdResult
{
    public IReadOnlyList<TagDto> Tags { get; set; } = [];
}