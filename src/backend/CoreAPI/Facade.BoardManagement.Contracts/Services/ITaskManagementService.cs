using Facade.BoardManagement.Contracts.Dtos;
using Facade.BoardManagement.Contracts.Parameters.Task;
using Facade.BoardManagement.Contracts.Results;

namespace Facade.BoardManagement.Contracts.Services;

public interface ITaskManagementService
{
    public Task<TaskDto> GetByIdAsync(GetTaskByIdParameters parameters);
    
    public Task<GetManyTasksByBoardIdResult> GetManyAsync(GetManyTasksByBoardIdParameters parameters);
    
    public Task<TaskDto> CreateAsync(CreateTaskParameters parameters);
    
    public Task<TaskDto> UpdateAsync(UpdateTaskParameters parameters);
    
    public Task DeleteAsync(DeleteTaskParameters parameters);
}