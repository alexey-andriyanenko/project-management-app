using Facade.ProjectManagement.Contracts.Dtos;

namespace Facade.ProjectManagement.Contracts.Results;

public class CreateManyProjectMembersResult
{
    public IReadOnlyList<ProjectMemberDto> ProjectMembers { get; set; } = [];
}