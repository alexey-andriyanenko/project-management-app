using Project.Client.Contracts.Resources;
using Project.Contracts.Dtos;
using Project.Contracts.Result;
using Project.Contracts.Service;

namespace Project.Client.Resources;

public class ProjectMemberResource(IProjectMemberService projectMemberService) : IProjectMemberResource
{
    public Task<GetManyProjectMembersByProjectIdResult> GetManyAsync(Project.Contracts.Parameters.ProjectMember.GetManyProjectMembersByProjectIdParameters parameters)
        => projectMemberService.GetManyAsync(parameters);
    
    public Task<ProjectMemberDto> GetAsync(Project.Contracts.Parameters.ProjectMember.GetProjectMemberByIdParameters parameters)
        => projectMemberService.GetAsync(parameters);
    
    public Task<ProjectMemberDto> CreateAsync(Project.Contracts.Parameters.ProjectMember.CreateProjectMemberParameters parameters)
        => projectMemberService.CreateAsync(parameters);
    
    public Task<ProjectMemberDto> UpdateAsync(Project.Contracts.Parameters.ProjectMember.UpdateProjectMemberParameters parameters)
        => projectMemberService.UpdateAsync(parameters);
    
    public Task DeleteAsync(Project.Contracts.Parameters.ProjectMember.DeleteProjectMemberParameters parameters)
        => projectMemberService.DeleteAsync(parameters);
}