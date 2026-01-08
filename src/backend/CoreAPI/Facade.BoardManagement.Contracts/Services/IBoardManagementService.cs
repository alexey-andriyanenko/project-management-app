using Facade.BoardManagement.Contracts.Dtos;
using Facade.BoardManagement.Contracts.Parameters.Board;
using Facade.BoardManagement.Contracts.Results;

namespace Facade.BoardManagement.Contracts.Services;

public interface IBoardManagementService
{
    public Task<BoardDto> GetByIdAsync(GetBoardByIdParameters parameters);
    
    public Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(GetManyBoardsByProjectIdParameters parameters);

    public Task<BoardDto> CreateAsync(CreateBoardParameters parameters);
    
    public Task<BoardDto> UpdateAsync(UpdateBoardParameters parameters);
    
    public Task DeleteAsync(DeleteBoardParameters parameters);
}