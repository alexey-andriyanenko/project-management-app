using Facade.ProjectManagement.Contracts.Dtos;

namespace Facade.ProjectManagement.Contracts.Results;

public class GetManyProjectMembersByProjectIdResult
{
    public IReadOnlyList<ProjectMemberDto> ProjectMembers { get; set; } = [];
}