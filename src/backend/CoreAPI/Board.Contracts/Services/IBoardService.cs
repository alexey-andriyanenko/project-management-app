using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;

namespace Board.Contracts.Services;

public interface IBoardService
{
    public Task<BoardDto> GetByIdAsync(GetBoardByIdParameters parameters);
    
    public Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(GetManyBoardsByProjectIdParameters parameters);

    public Task<BoardDto> CreateAsync(CreateBoardParameters parameters);
    
    public Task<BoardDto> UpdateAsync(UpdateBoardParameters parameters);
    
    public Task DeleteAsync(DeleteBoardParameters parameters);
    
    public Task DeleteManyAsync(DeleteManyBoardsByTenantId parameters, CancellationToken cancellationToken);
    
    public Task DeleteManyAsync(DeleteManyBoardsByProjectId parameters, CancellationToken cancellationToken);
    
    public Task SeedBoardTypesForTenantAsync(Guid tenantId, CancellationToken cancellationToken);
}
