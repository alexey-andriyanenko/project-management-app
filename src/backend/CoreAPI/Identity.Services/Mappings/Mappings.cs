using Identity.Contracts.Dtos;
using Identity.DataAccess.Entities;

namespace Identity.Services.Mappings;

public static class Mappings
{
    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email!,
            UserName = entity.UserName!,
        };
    }
}