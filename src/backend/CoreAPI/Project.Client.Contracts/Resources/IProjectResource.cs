using Project.Contracts.Dtos;
using Project.Contracts.Parameters;
using Project.Contracts.Parameters.Project;
using Project.Contracts.Result;

namespace Project.Client.Contracts.Resources;

public interface IProjectResource
{
    public Task<ProjectDto> GetAsync(GetProjectByIdParameters parameters);
    
    public Task<ProjectDto> GetAsync(GetProjectBySlugParameters parameters);

    public Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters);

    public Task<ProjectDto> CreateAsync(CreateProjectParameters parameters);
    
    public Task<ProjectDto> UpdateAsync(UpdateProjectParameters parameters);
    
    public Task DeleteAsync(DeleteProjectParameters parameters);
}