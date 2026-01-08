using Tag.Contracts.Dtos;

namespace Tag.Contracts.Result;

public class GetManyTagsByIdsResult
{
    public IReadOnlyList<TagDto> Tags { get; set; } = [];
}