using Board.Client.Contracts.Resources;
using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;
using Board.Contracts.Services;

namespace Board.Client.Resources;

public class BoardResource(IBoardService boardService) : IBoardResource
{
    public Task<BoardDto> GetByIdAsync(GetBoardByIdParameters parameters)
        => boardService.GetByIdAsync(parameters);

    public Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(
        GetManyBoardsByProjectIdParameters parameters)
        => boardService.GetManyByProjectIdAsync(parameters);

    public Task<BoardDto> CreateAsync(CreateBoardParameters parameters)
        => boardService.CreateAsync(parameters);

    public Task<BoardDto> UpdateAsync(UpdateBoardParameters parameters)
        => boardService.UpdateAsync(parameters);

    public Task DeleteAsync(DeleteBoardParameters parameters)
        => boardService.DeleteAsync(parameters);
}
