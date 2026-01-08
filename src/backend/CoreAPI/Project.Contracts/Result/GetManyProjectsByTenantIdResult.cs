using Project.Contracts.Dtos;

namespace Project.Contracts.Result;

public class GetManyProjectsByTenantIdResult
{
    public IReadOnlyList<ProjectDto> Projects { get; set; } = [];
}