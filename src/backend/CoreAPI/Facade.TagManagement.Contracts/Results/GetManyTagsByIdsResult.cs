using Facade.TagManagement.Contracts.Dtos;

namespace Facade.TagManagement.Contracts.Results;

public class GetManyTagsByIdsResult
{
    public IReadOnlyList<TagDto> Tags { get; set; } = [];
}