using Project.Contracts.Dtos;

namespace Project.Contracts.Result;

public class CreateManyProjectMembersResult
{
    public IReadOnlyList<ProjectMemberDto> ProjectMembers { get; set; }
}