using Infrastructure.EventBus.Contracts;
using Microsoft.EntityFrameworkCore;
using Project.Contracts.Dtos;
using Project.Contracts.Exceptions;
using Project.Contracts.Parameters.Project;
using Project.Contracts.Result;
using Project.Contracts.Service;
using Project.DataAccess;
using Project.DataAccess.Entities;
using Project.Events.Contracts;
using Project.Services.Mappings;
using Shared.String.Extensions;

namespace Project.Services;

public class ProjectService(ProjectDbContext dbContext, IEventBus eventBus) : IProjectService
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

    public async Task<ProjectDto> GetAsync(GetProjectBySlugParameters parameters)
    {
        var project = await dbContext.Projects
            .AsNoTracking()
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId && x.Slug == parameters.Slug);
        if (project == null)
        {
            throw new ProjectNotFoundException(parameters.Slug);
        }
        
        var member = await dbContext.ProjectMembers.FirstOrDefaultAsync(x => x.ProjectId == project.Id && x.UserId == parameters.MemberId);
        if (member == null)
        {
            throw new ProjectNotFoundException(parameters.Slug);
        }
        
        return project.ToDto();
    }

    public async Task<GetManyProjectsByTenantIdResult> GetManyAsync(GetManyProjectsByTenantIdParameters parameters)
    {
        var projectIds = await dbContext.ProjectMembers
            .AsNoTracking()
            .Where(x => x.UserId == parameters.UserId)
            .Select(x => x.ProjectId)
            .ToListAsync();

        if (!projectIds.Any())
        {
            return new GetManyProjectsByTenantIdResult
            {
                Projects = new List<ProjectDto>()
            };
        }
        
        var projects = await dbContext.Projects
            .AsNoTracking()
            .Where(x => x.TenantId == parameters.TenantId && projectIds.Contains(x.Id))
            .ToListAsync();
        
        return new GetManyProjectsByTenantIdResult
        {
            Projects = projects.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<ProjectDto> CreateAsync(CreateProjectParameters parameters)
    {
        if (parameters.Members.Any(m => m.UserId == parameters.UserId))
        {
            throw new ArgumentException("The project owner cannot be added as a member.");
        }
        
        var projectId = Guid.NewGuid();
        var project = new ProjectEntity() 
        {
            Id = projectId,
            TenantId = parameters.TenantId,
            Name = parameters.Name,
            Description = parameters.Description,
            Slug = parameters.Name.ToSlug(),
        };

        project.Members.Add(new ProjectMemberEntity()
        {
            UserId = parameters.UserId,
            ProjectId = projectId,
            Role = DataAccess.Enums.ProjectMemberRole.Owner
        });
        
        if (project.Members.Count > 0)
        {
            foreach (var member in parameters.Members)
            {
                project.Members.Add(new ProjectMemberEntity()
                {
                    UserId = member.UserId,
                    ProjectId = projectId,
                    Role = (DataAccess.Enums.ProjectMemberRole)member.Role
                });
            }
        }
        
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        await eventBus.PublishAsync(new ProjectCreatedEvent()
        {
            ProjectId = project.Id,
            TenantId = project.TenantId,
        });

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