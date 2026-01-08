using Identity.Contracts.Dtos;
using Identity.Contracts.Exceptions;
using Identity.Contracts.Parameters.User;
using Identity.Contracts.Results;
using Identity.Contracts.Services;
using Identity.DataAccess;
using Identity.DataAccess.Entities;
using Identity.Services.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services;

public class UserService(IdentityDbContext dbContext) : IUserService
{
    public async Task<UserDto> GetAsync(GetUserByIdParameters parameters)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == parameters.Id);

        if (user == null)
        {
            throw new UserNotFoundException(parameters.Id);
        }

        return user.ToDto();
    }
    
    public async Task<GetManyUsersByIdResults> GetManyAsync(GetManyUsersByIdsParameters parameters) 
    {
        var users = await dbContext.Users
            .Where(x => parameters.Ids.Contains(x.Id))
            .ToListAsync();

        var notFoundIds = parameters.Ids.Except(users.Select(x => x.Id)).ToList();
        
        if (notFoundIds.Any())
        {
            throw new UserNotFoundException(notFoundIds.First());
        }

        return new GetManyUsersByIdResults
        {
            Users = users.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<UserDto> CreateAsync(CreateUserParameters parameters)
    {
        var user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            Email = parameters.Email,
            UserName = parameters.UserName,
        };

        var passwordHasher = new PasswordHasher<UserEntity>();
        var passwordHash = passwordHasher.HashPassword(user, parameters.Password);
        
        user.PasswordHash = passwordHash;
        
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        return user.ToDto();
    }
    
    public async Task<UserDto> UpdateAsync(UpdateUserParameters parameters)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == parameters.Id);

        if (user == null)
        {
            throw new UserNotFoundException(parameters.Id);
        }

        user.FirstName = parameters.FirstName;
        user.LastName = parameters.LastName;
        user.Email = parameters.Email;
        user.UserName = parameters.UserName;

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();

        return user.ToDto();
    }
}