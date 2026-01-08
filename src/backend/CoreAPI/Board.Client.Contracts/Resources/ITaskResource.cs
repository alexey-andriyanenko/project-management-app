using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Task;
using Board.Contracts.Results;

namespace Board.Client.Contracts.Resources;

public interface ITaskResource
{
    public Task<TaskDto> GetByIdAsync(GetTaskByIdParameters parameters);
    
    public Task<GetManyTasksByBoardIdResult> GetManyAsync(GetManyTasksByBoardIdParameters parameters);
    
    public Task<TaskDto> CreateAsync(CreateTaskParameters parameters);
    
    public Task<TaskDto> UpdateAsync(UpdateTaskParameters parameters);
    
    public Task DeleteAsync(DeleteTaskParameters parameters);
}