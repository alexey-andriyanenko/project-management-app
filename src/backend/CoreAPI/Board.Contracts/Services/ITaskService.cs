using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Task;
using Board.Contracts.Results;

namespace Board.Contracts.Services;

public interface ITaskService
{
    public Task<TaskDto> GetByIdAsync(GetTaskByIdParameters parameters);
    
    public Task<GetManyTasksByBoardIdResult> GetManyAsync(GetManyTasksByBoardIdParameters parameters);
    
    public Task<TaskDto> CreateAsync(CreateTaskParameters parameters);
    
    public Task<TaskDto> UpdateAsync(UpdateTaskParameters parameters);
    
    public Task DeleteAsync(DeleteTaskParameters parameters);
}
