using Project.Contracts.Dtos;
using Project.Contracts.Parameters.ProjectMember;
using Project.Contracts.Result;

namespace Project.Contracts.Service;

public interface IProjectMemberService
{
    public Task<GetManyProjectMembersByProjectIdResult> GetManyAsync(GetManyProjectMembersByProjectIdParameters parameters);
    
    public Task<ProjectMemberDto> GetAsync(GetProjectMemberByIdParameters parameters);
    
    public Task<ProjectMemberDto> CreateAsync(CreateProjectMemberParameters parameters);
    
    public Task<CreateManyProjectMembersResult> CreateManyAsync(CreateManyProjectMembersParameters parameters);
    
    public Task<ProjectMemberDto> UpdateAsync(UpdateProjectMemberParameters parameters);
    
    public Task DeleteAsync(DeleteProjectMemberParameters parameters);
}