using Board.Client.Contracts;
using Facade.BoardManagement.Contracts.Dtos;
using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Facade.BoardManagement.Services.Mappings.Board;

namespace Facade.BoardManagement.Services;

public class BoardManagementService(IBoardClient boardClient) : IBoardManagementService
{
    public async Task<BoardDto> GetByIdAsync(Contracts.Parameters.Board.GetBoardByIdParameters parameters)
    {
        var result = await boardClient.BoardResource.GetByIdAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }
    
    public async Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(Contracts.Parameters.Board.GetManyBoardsByProjectIdParameters parameters)
    {
        var result = await boardClient.BoardResource.GetManyByProjectIdAsync(parameters.ToCoreParameters());
        
        return new GetManyBoardsByProjectIdResult() 
        { 
            Boards = result.Boards.Select(b => b.ToFacadeDto()).ToList() 
        };
    }
    
    public async Task<BoardDto> CreateAsync(Contracts.Parameters.Board.CreateBoardParameters parameters)
    {
        var result = await boardClient.BoardResource.CreateAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }
    
    public async Task<BoardDto> UpdateAsync(Contracts.Parameters.Board.UpdateBoardParameters parameters)
    {
        var result = await boardClient.BoardResource.UpdateAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }
    
    public async Task DeleteAsync(Contracts.Parameters.Board.DeleteBoardParameters parameters)
    {
        await boardClient.BoardResource.DeleteAsync(parameters.ToCoreParameters());
    }
}