using Microsoft.EntityFrameworkCore;
using Project.Contracts.Dtos;
using Project.Contracts.Exceptions;
using Project.Contracts.Parameters;
using Project.Contracts.Parameters.Project;
using Project.Contracts.Result;
using Project.Contracts.Service;
using Project.DataAccess;
using Project.DataAccess.Entities;
using Project.Services.Mappings;
using Shared.String.Extensions;

namespace Project.Services;

public class ProjectService(ProjectDbContext dbContext) : IProjectService
{
    public async Task<ProjectDto> GetAsync(GetProjectByIdParameters parameters)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId && x.Id == parameters.ProjectId);
      
        if (project == null)
        {
            throw new ProjectNotFoundException(parameters.ProjectId);
        }

        return project.ToDto();
    }
    
    public async Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters)
    {
        var projects = await dbContext.Projects
            .Where(x => x.TenantId == parameters.TenantId)
            .ToListAsync();

        return new GetManyProjectsByTenantIdResult
        {
            Projects = projects.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<ProjectDto> CreateAsync(CreateProjectParameters parameters)
    {
        var projectId = Guid.NewGuid();
        var project = new ProjectEntity() 
        {
            Id = projectId,
            TenantId = parameters.TenantId,
            Name = parameters.Name,
            Description = parameters.Description,
            Slug = parameters.Name.ToSlug(),
            Members = parameters.Members.Select(x => new ProjectMemberEntity()
            {
                ProjectId = projectId,
                UserId = x.UserId,
                Role = (DataAccess.Enums.ProjectMemberRole)x.Role,
            }).ToList(),
        };
        
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        return project.ToDto();
    }

    public async Task<ProjectDto> UpdateAsync(UpdateProjectParameters parameters)
    {
        var project = await dbContext.Projects
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId && x.Id == parameters.ProjectId);

        if (project == null)
        {
            throw new ProjectNotFoundException(parameters.ProjectId);
        }
        
        project.Name = parameters.Name;
        project.Description = parameters.Description;
        project.Slug = parameters.Name.ToSlug();
        
        await dbContext.SaveChangesAsync();
        
        return project.ToDto();
    }
    
    public async Task DeleteAsync(DeleteProjectParameters parameters)
    {
        var project = await dbContext.Projects
            .FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId && x.Id == parameters.ProjectId);

        if (project == null)
        {
            throw new ProjectNotFoundException(parameters.ProjectId);
        }
        
        dbContext.Projects.Remove(project);
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteManyAsync(DeleteManyProjectsByTenantIdParameters parameters, CancellationToken cancellationToken = default)
    {
        await dbContext.Projects
            .Where(x => x.TenantId == parameters.TenantId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}