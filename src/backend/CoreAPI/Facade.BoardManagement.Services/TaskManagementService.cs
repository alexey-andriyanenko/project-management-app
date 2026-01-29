using Board.Client.Contracts;
using Facade.BoardManagement.Contracts.Dtos;
using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Facade.BoardManagement.Services.Mappings.Task;
using Identity.Client.Contracts;
using Identity.Contracts.Parameters.User;
using Tag.Client.Contracts;
using Tag.Contracts.Parameters;

namespace Facade.BoardManagement.Services;

public class TaskManagementService(IBoardClient boardClient, ITagClient tagClient, IIdentityClient identityClient)
    : ITaskManagementService
{
    public async Task<TaskDto> GetByIdAsync(Contracts.Parameters.Task.GetTaskByIdParameters parameters)
    {
        var task = await boardClient.TaskResource.GetByIdAsync(parameters.ToCoreParameters());
        var enrichedTasks = await EnrichCoreTaskDtos([task], parameters.TenantId);
        
        return enrichedTasks.First();
    }

    public async Task<GetManyTasksByBoardIdResult> GetManyAsync(
        Contracts.Parameters.Task.GetManyTasksByBoardIdParameters parameters)
    {
        var result = await boardClient.TaskResource.GetManyAsync(parameters.ToCoreParameters());
        var enrichedTasks = await EnrichCoreTaskDtos(result.Tasks, parameters.TenantId);
        
        return new GetManyTasksByBoardIdResult()
        {
            Tasks = enrichedTasks
        };
    }

    public async Task<TaskDto> CreateAsync(Contracts.Parameters.Task.CreateTaskParameters parameters)
    {
        var task = await boardClient.TaskResource.CreateAsync(parameters.ToCoreParameters());
        var enrichedTasks = await EnrichCoreTaskDtos([task], parameters.TenantId);
        
        return enrichedTasks.First();
    }

    public async Task<TaskDto> UpdateAsync(Contracts.Parameters.Task.UpdateTaskParameters parameters)
    {
        var task = await boardClient.TaskResource.UpdateAsync(parameters.ToCoreParameters());
        
        var enrichedTasks = await EnrichCoreTaskDtos([task], parameters.TenantId);
        return enrichedTasks.First();
    }

    public async Task DeleteAsync(Contracts.Parameters.Task.DeleteTaskParameters parameters)
    {
        await boardClient.TaskResource.DeleteAsync(parameters.ToCoreParameters());
    }


    private async Task<IReadOnlyList<TaskDto>> EnrichCoreTaskDtos(IReadOnlyList<Board.Contracts.Dtos.TaskDto> tasks, Guid tenantId)
    {
        var allUserIds = tasks
            .SelectMany(t =>
            {
                var ids = new List<Guid>() { t.CreatedByUserId };
                
                if (t.AssignedToUserId.HasValue)
                {
                    ids.Add(t.AssignedToUserId.Value);
                }

                return ids;
            })
            .Distinct()
            .ToList();
        
        var tagsTask = tagClient.TagResource.GetManyAsync(new GetManyTagsByIdsParameters()
        {
            TenantId = tenantId,
            TagIds = tasks.SelectMany(t => t.TagIds).Distinct().ToList()
        });
        var usersTask = identityClient.UserResource.GetManyAsync(new GetManyUsersByIdsParameters()
        {
            Ids = allUserIds
        });

        await Task.WhenAll(tagsTask, usersTask);

        var tags = tagsTask.Result.Tags.ToDictionary(tag => tag.Id, tag => tag);
        var users = usersTask.Result.Users.ToDictionary(u => u.Id, u => u);
        
        var taskDtos = tasks.Select(t =>
        {
            var createdByUser = users[t.CreatedByUserId];
            var assignedToUser = t.AssignedToUserId.HasValue ? users[t.AssignedToUserId.Value] : null;
            var taskTags = t.TagIds.Select(tagId => tags[tagId]).ToList();

            return t.ToFacadeDto(taskTags, createdByUser, assignedToUser);
        }).ToList();

        return taskDtos;
    }
}