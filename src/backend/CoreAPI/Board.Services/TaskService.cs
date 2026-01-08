using Board.Contracts.Dtos;
using Board.Contracts.Exceptions;
using Board.Contracts.Parameters.Task;
using Board.Contracts.Results;
using Board.Contracts.Services;
using Board.DataAccess;
using Board.DataAccess.Entities;
using Board.Services.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Board.Services;

public class TaskService(BoardDbContext dbContext) : ITaskService
{
    public async Task<TaskDto> GetByIdAsync(GetTaskByIdParameters parameters)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == parameters.Id && x.TenantId == parameters.TenantId && x.ProjectId == parameters.ProjectId && x.BoardId == parameters.BoardId);
        
        if (task == null)
        {
            throw new TaskNotFoundException(parameters.Id, parameters.TenantId);
        }

        return task.ToDto();
    }
    
    public async Task<GetManyTasksByBoardIdResult> GetManyAsync(GetManyTasksByBoardIdParameters parameters)
    {
        var tasks = await dbContext.Tasks
            .Where(x => x.TenantId == parameters.TenantId && x.ProjectId == parameters.ProjectId && x.BoardId == parameters.BoardId)
            .ToListAsync();

        return new GetManyTasksByBoardIdResult
        {
            Tasks = tasks.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<TaskDto> CreateAsync(CreateTaskParameters parameters)
    {
        var task = new TaskEntity()
        {
            Id = Guid.NewGuid(),
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            BoardId = parameters.BoardId,
            BoardColumnId = parameters.BoardColumnId,
            CreatedByUserId = parameters.CreatorUserId,
            AssigneeUserId = parameters.AssigneeUserId,
            Title = parameters.Title,
            DescriptionAsJson = parameters.DescriptionAsJson,
            DescriptionAsPlainText = parameters.DescriptionAsPlainText,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();

        return task.ToDto();
    }
    
    public async Task<TaskDto> UpdateAsync(UpdateTaskParameters parameters) 
    {
        var task = await dbContext.Tasks
            .FirstOrDefaultAsync(t => t.TenantId == parameters.TenantId && t.ProjectId == parameters.ProjectId && t.Id == parameters.Id && t.BoardId == parameters.BoardId);

        if (task == null)
        {
            throw new TaskNotFoundException(parameters.Id, parameters.TenantId);
        }
        
        task.Title = parameters.Title;
        task.DescriptionAsJson = parameters.DescriptionAsJson;
        task.DescriptionAsPlainText = parameters.DescriptionAsPlainText;
        task.BoardColumnId = parameters.BoardColumnId;
        task.AssigneeUserId = parameters.AssigneeUserId;
        task.UpdatedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();

        return task.ToDto();
    }
    
    public async Task DeleteAsync(DeleteTaskParameters parameters)
    {
        var task = await dbContext.Tasks
            .FirstOrDefaultAsync(t => t.TenantId == parameters.TenantId && t.ProjectId == parameters.ProjectId && t.Id == parameters.Id && t.BoardId == parameters.BoardId);

        if (task == null)
        {
            throw new TaskNotFoundException(parameters.Id, parameters.TenantId);
        }

        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
    }
}