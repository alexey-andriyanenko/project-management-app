using Facade.ProjectManagement.Contracts.Dtos;
using Facade.ProjectManagement.Contracts.Parameters.ProjectMember;
using Facade.ProjectManagement.Contracts.Results;

namespace Facade.ProjectManagement.Contracts.Services;

public interface IProjectMemberManagementService
{
    public Task<GetManyProjectMembersByProjectIdResult> GetManyAsync(GetManyProjectMembersByProjectIdParameters parameters);
    
    public Task<ProjectMemberDto> GetAsync(GetProjectMemberByIdParameters parameters);
    
    public Task<ProjectMemberDto> CreateAsync(CreateProjectMemberParameters parameters);
    
    public Task<CreateManyProjectMembersResult> CreateManyAsync(CreateManyProjectMembersParameters parameters);
    
    public Task<ProjectMemberDto> UpdateAsync(UpdateProjectMemberParameters parameters);
    
    public Task DeleteAsync(DeleteProjectMemberParameters parameters);
}