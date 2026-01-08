using Identity.Contracts.Dtos;

namespace Identity.Contracts.Results;

public class GetManyUsersByIdResults
{
    public IReadOnlyList<UserDto> Users { get; set; } = [];
}