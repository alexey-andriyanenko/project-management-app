namespace Facade.IdentityManagement.Services.Mappings.Dtos;

public static class DtosMappings
{
    public static Facade.IdentityManagement.Contracts.Dtos.UserDto ToFacadeDto(this Identity.Contracts.Dtos.UserDto dto)
    {
        return new Facade.IdentityManagement.Contracts.Dtos.UserDto
        {
            Id = dto.Id,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
        };
    }
}