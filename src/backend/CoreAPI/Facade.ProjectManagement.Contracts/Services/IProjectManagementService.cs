using Facade.ProjectManagement.Contracts.Dtos;
using Facade.ProjectManagement.Contracts.Parameters.Project;
using Facade.ProjectManagement.Contracts.Results;

namespace Facade.ProjectManagement.Contracts.Services;

public interface IProjectManagementService
{
    public Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters);
    
    public Task<ProjectDto> GetAsync(GetProjectByIdParameters parameters);
    
    public Task<ProjectDto> CreateAsync(CreateProjectParameters parameters);
    
    public Task<ProjectDto> UpdateAsync(UpdateProjectParameters parameters);
    
    public Task DeleteAsync(DeleteProjectParameters parameters);
}