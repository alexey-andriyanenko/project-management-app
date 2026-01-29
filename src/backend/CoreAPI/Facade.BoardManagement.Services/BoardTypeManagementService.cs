using Board.Client.Contracts;
using Facade.BoardManagement.Contracts.Parameters.BoardType;
using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Facade.BoardManagement.Services.Mappings.Board;

namespace Facade.BoardManagement.Services;

public class BoardTypeManagementService(IBoardClient boardClient) : IBoardTypeManagementService
{
    public async Task<GetManyBoardTypesByTenantIdResult> GetManyAsync(GetManyBoardTypesByTenantIdParameters parameters)
    {
        var result = await boardClient.BoardTypeResource.GetManyAsync(parameters.ToCoreParameters());
        
        return new GetManyBoardTypesByTenantIdResult() 
        { 
            BoardTypes = result.BoardTypes.Select(bt => bt.ToFacadeDto()).ToList() 
        };
    }
}
