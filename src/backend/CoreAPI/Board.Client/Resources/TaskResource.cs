using Board.Client.Contracts.Resources;
using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Task;
using Board.Contracts.Results;
using Board.Contracts.Services;

namespace Board.Client.Resources;

public class TaskResource(ITaskService taskService) : ITaskResource
{
    public async Task<TaskDto> GetByIdAsync(GetTaskByIdParameters parameters)
        => await taskService.GetByIdAsync(parameters);
    
    public async Task<GetManyTasksByBoardIdResult> GetManyAsync(GetManyTasksByBoardIdParameters parameters)
        => await taskService.GetManyAsync(parameters);
    
    public async Task<TaskDto> CreateAsync(CreateTaskParameters parameters)
        => await taskService.CreateAsync(parameters);
    
    public async Task<TaskDto> UpdateAsync(UpdateTaskParameters parameters)
        => await taskService.UpdateAsync(parameters);
    
    public async Task DeleteAsync(DeleteTaskParameters parameters)
        => await taskService.DeleteAsync(parameters);
}