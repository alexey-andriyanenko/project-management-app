using Facade.ProjectManagement.Contracts.Dtos;

namespace Facade.ProjectManagement.Contracts.Results;

public class GetManyProjectsByTenantIdResult
{
    public IReadOnlyList<ProjectDto> Projects { get; set; } = [];
}