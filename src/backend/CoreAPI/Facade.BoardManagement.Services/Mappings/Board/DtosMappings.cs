using Board.Contracts.Dtos;

namespace Facade.BoardManagement.Services.Mappings.Board;

public static class DtosMappings
{
    public static Facade.BoardManagement.Contracts.Dtos.BoardDto ToFacadeDto(this BoardDto coreApiDto)
    {
        return new Facade.BoardManagement.Contracts.Dtos.BoardDto
        {
            Id = coreApiDto.Id,
            ProjectId = coreApiDto.ProjectId,
            TenantId = coreApiDto.TenantId,
            CreatedByUserId = coreApiDto.CreatedByUserId,
            Columns = coreApiDto.Columns.Select(x => x.ToFacadeDto()).ToList(),
            Type = coreApiDto.Type.ToFacadeDto(),
            CreatedAt = coreApiDto.CreatedAt,
            UpdatedAt = coreApiDto.UpdatedAt,
        };
    }
    
    private static Facade.BoardManagement.Contracts.Dtos.BoardColumnDto ToFacadeDto(this BoardColumnDto coreApiDto)
    {
        return new Facade.BoardManagement.Contracts.Dtos.BoardColumnDto
        {
            Id = coreApiDto.Id,
            BoardId = coreApiDto.BoardId,
            CreatedByUserId = coreApiDto.CreatedByUserId,
            Name = coreApiDto.Name,
            Order = coreApiDto.Order,
            CreatedAt = coreApiDto.CreatedAt,
            UpdatedAt = coreApiDto.UpdatedAt
        };
    }
    
    private static Facade.BoardManagement.Contracts.Dtos.BoardTypeDto ToFacadeDto(this BoardTypeDto coreApiDto)
    {
        return new Facade.BoardManagement.Contracts.Dtos.BoardTypeDto
        {
            Id = coreApiDto.Id,
            TenantId = coreApiDto.TenantId,
            ProjectId = coreApiDto.ProjectId,
            IsEssential = coreApiDto.IsEssential,
            Name = coreApiDto.Name
        };
    }
}