namespace Facade.IdentityManagement.Contracts.Parameters.User;

public class GetManyUsersByIdsParameters
{
    public IReadOnlyList<Guid> Ids { get; set; } = [];
}