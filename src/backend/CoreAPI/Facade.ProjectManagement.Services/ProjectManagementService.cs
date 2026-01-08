using Facade.ProjectManagement.Contracts.Dtos;
using Facade.ProjectManagement.Contracts.Parameters.Project;
using Facade.ProjectManagement.Contracts.Results;
using Facade.ProjectManagement.Contracts.Services;
using Facade.ProjectManagement.Services.Mappings.Dtos;
using Facade.ProjectManagement.Services.Mappings.Parameters;

namespace Facade.ProjectManagement.Services;

public class ProjectManagementService(Project.Client.Contracts.IProjectClient projectClient) : IProjectManagementService
{
    public async Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters)
    {
        var result = await projectClient.ProjectResource.GetManyAsync(parameters.ToCoreParameters());
        
        return new GetManyProjectsByTenantIdResult
        {
            Projects = result.Projects.Select(x => x.ToFacadeDto()).ToList()
        };
    }
    
    public async Task<ProjectDto> GetAsync(GetProjectByIdParameters parameters)
    {
        try
        {
            var project = await projectClient.ProjectResource.GetAsync(parameters.ToCoreParameters());
            return project.ToFacadeDto();
        }
        catch(Project.Contracts.Exceptions.ProjectNotFoundException) 
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectNotFoundException(parameters.ProjectId);
        }
    }
    
    public async Task<ProjectDto> CreateAsync(CreateProjectParameters parameters)
    {
        try
        {
            var project = await projectClient.ProjectResource.CreateAsync(parameters.ToCoreParameters());
            return project.ToFacadeDto();
        }
        catch(Project.Contracts.Exceptions.ProjectNotFoundException) 
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectNotFoundException(parameters.TenantId);
        }
    }
    
    public async Task<ProjectDto> UpdateAsync(UpdateProjectParameters parameters)
    {
        try
        {
            var project = await projectClient.ProjectResource.UpdateAsync(parameters.ToCoreParameters());
            return project.ToFacadeDto();
        }
        catch(Project.Contracts.Exceptions.ProjectNotFoundException) 
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectNotFoundException(parameters.ProjectId);
        }
    }
    
    public async Task DeleteAsync(DeleteProjectParameters parameters)
    {
        try 
        {
            await projectClient.ProjectResource.DeleteAsync(parameters.ToCoreParameters());
        }
        catch(Project.Contracts.Exceptions.ProjectNotFoundException) 
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectNotFoundException(parameters.ProjectId);
        }
    }
}