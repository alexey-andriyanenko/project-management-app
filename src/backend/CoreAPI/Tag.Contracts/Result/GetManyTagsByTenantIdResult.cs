using Tag.Contracts.Dtos;

namespace Tag.Contracts.Result;

public class GetManyTagsByTenantIdResult
{
    public IReadOnlyList<TagDto> Tags { get; set; } = [];
}