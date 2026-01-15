using Project.Contracts.Dtos;
using Project.Contracts.Parameters;
using Project.Contracts.Parameters.Project;
using Project.Contracts.Result;

namespace Project.Contracts.Service;

public interface IProjectService
{
    public Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters);
    
    public Task<ProjectDto> GetAsync(GetProjectByIdParameters parameters);
    
    public Task<ProjectDto> GetAsync(GetProjectBySlugParameters parameters);
    
    public Task<ProjectDto> CreateAsync(CreateProjectParameters parameters);
    
    public Task<ProjectDto> UpdateAsync(UpdateProjectParameters parameters);
    
    public Task DeleteAsync(DeleteProjectParameters parameters);
    
    public Task DeleteManyAsync(DeleteManyProjectsByTenantIdParameters parameters, CancellationToken cancellationToken = default);
}