using Board.Contracts.Parameters.Board;

namespace Facade.BoardManagement.Services.Mappings.Board;

public static class ParametersMappings
{
    public static GetManyBoardTypesParameters ToCoreParameters(this Contracts.Parameters.BoardType.GetManyBoardTypesByTenantIdParameters parameters)
    {
        return new GetManyBoardTypesParameters
        {
            TenantId = parameters.TenantId
        };
    }
    
    public static GetBoardByIdParameters ToCoreParameters(this Contracts.Parameters.Board.GetBoardByIdParameters parameters)
    {
        return new GetBoardByIdParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
    
    public static GetManyBoardsByProjectIdParameters ToCoreParameters(this Contracts.Parameters.Board.GetManyBoardsByProjectIdParameters parameters)
    {
        return new GetManyBoardsByProjectIdParameters
        {
            ProjectId = parameters.ProjectId,
            TenantId = parameters.TenantId,
        };
    }
    
    public static CreateBoardParameters ToCoreParameters(this Contracts.Parameters.Board.CreateBoardParameters parameters)
    {
        return new CreateBoardParameters
        {
            Name = parameters.Name,
            BoardTypeId = parameters.BoardTypeId,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            UserId = parameters.UserId,
            Columns = parameters.Columns.Select(c => new CreateBoardColumnItemParameters 
            {
                Name = c.Name
            }).ToList()
        };
    }
    
    public static UpdateBoardParameters ToCoreParameters(this Contracts.Parameters.Board.UpdateBoardParameters parameters)
    {
        return new UpdateBoardParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            UserId = parameters.UserId,
            Name = parameters.Name,
            Columns = parameters.Columns.Select(c => new UpdateBoardColumnItemParameters 
            {
                Name = c.Name
            }).ToList()
        };
    }
    
    public static DeleteBoardParameters ToCoreParameters(this Contracts.Parameters.Board.DeleteBoardParameters parameters)
    {
        return new DeleteBoardParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
}