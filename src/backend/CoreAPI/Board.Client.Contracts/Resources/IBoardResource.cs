using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;

namespace Board.Client.Contracts.Resources;

public interface IBoardResource
{
    public Task<BoardDto> GetByIdAsync(GetBoardByIdParameters parameters);
    
    public Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(GetManyBoardsByProjectIdParameters parameters);

    public Task<BoardDto> CreateAsync(CreateBoardParameters parameters);
    
    public Task<BoardDto> UpdateAsync(UpdateBoardParameters parameters);
    
    public Task DeleteAsync(DeleteBoardParameters parameters);
}
