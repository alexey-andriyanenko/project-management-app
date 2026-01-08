using Facade.IdentityManagement.Contracts.Dtos;

namespace Facade.IdentityManagement.Contracts.Results;

public class GetManyUsersByIdResult
{
    public IReadOnlyList<UserDto> Users { get; set; } = [];
}