using Project.Contracts.Dtos;

namespace Project.Contracts.Result;

public class GetManyProjectMembersByProjectIdResult
{
    public IReadOnlyList<ProjectMemberDto> ProjectMembers { get; set; } = [];
}