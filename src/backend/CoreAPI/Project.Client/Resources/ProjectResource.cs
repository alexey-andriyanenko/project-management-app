using Project.Client.Contracts.Resources;
using Project.Contracts.Dtos;
using Project.Contracts.Parameters;
using Project.Contracts.Parameters.Project;
using Project.Contracts.Result;
using Project.Contracts.Service;

namespace Project.Client.Resources;

public class ProjectResource(IProjectService projectService) : IProjectResource
{
    public Task<ProjectDto> GetAsync(GetProjectByIdParameters parameters)
        => projectService.GetAsync(parameters);
    
    public Task<ProjectDto> GetAsync(GetProjectBySlugParameters parameters)
        => projectService.GetAsync(parameters);

    public Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters)
        => projectService.GetManyAsync(parameters);
    
    public Task<ProjectDto> CreateAsync(CreateProjectParameters parameters)
        => projectService.CreateAsync(parameters);
    
    public Task<ProjectDto> UpdateAsync(UpdateProjectParameters parameters)
        => projectService.UpdateAsync(parameters);
    
    public Task DeleteAsync(DeleteProjectParameters parameters)
        => projectService.DeleteAsync(parameters);
} 