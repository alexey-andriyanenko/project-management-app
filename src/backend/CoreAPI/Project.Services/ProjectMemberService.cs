using Microsoft.EntityFrameworkCore;
using Project.Contracts.Dtos;
using Project.Contracts.Exceptions;
using Project.Contracts.Parameters.ProjectMember;
using Project.Contracts.Result;
using Project.Contracts.Service;
using Project.DataAccess;
using Project.Services.Mappings;

namespace Project.Services;

public class ProjectMemberService(ProjectDbContext dbContext) : IProjectMemberService
{
    public async Task<GetManyProjectMembersByProjectIdResult> GetManyAsync(
        GetManyProjectMembersByProjectIdParameters parameters)
    {
        var members = await dbContext.ProjectMembers
            .Where(x => x.ProjectId == parameters.ProjectId)
            .ToListAsync();

        return new GetManyProjectMembersByProjectIdResult
        {
            ProjectMembers = members.Select(x => x.ToDto()).ToList()
        };
    }

    public async Task<ProjectMemberDto> GetAsync(GetProjectMemberByIdParameters parameters)
    {
        var member = await dbContext.ProjectMembers
            .FirstOrDefaultAsync(x => x.ProjectId == parameters.ProjectId && x.UserId == parameters.UserId);

        if (member == null)
        {
            throw new ProjectMemberNotFoundException(parameters.ProjectId, parameters.UserId);
        }

        return member.ToDto();
    }

    public async Task<ProjectMemberDto> CreateAsync(CreateProjectMemberParameters parameters)
    {
        var result = await CreateManyAsync(new CreateManyProjectMembersParameters
        {
            ProjectId = parameters.ProjectId,
            Members = new List<CreateProjectMemberItemParameters>
            {
                new CreateProjectMemberItemParameters
                {
                    UserId = parameters.UserId,
                    Role = parameters.Role
                }
            }
        });

        return result.ProjectMembers.First();
    }

    public async Task<CreateManyProjectMembersResult> CreateManyAsync(CreateManyProjectMembersParameters parameters)
    {
        var createdMembers = new List<ProjectMemberDto>();

        foreach (var memberParam in parameters.Members)
        {
            var member = new DataAccess.Entities.ProjectMemberEntity
            {
                ProjectId = parameters.ProjectId,
                UserId = memberParam.UserId,
                Role = (DataAccess.Enums.ProjectMemberRole)memberParam.Role,
            };

            dbContext.ProjectMembers.Add(member);
            createdMembers.Add(member.ToDto());
        }

        await dbContext.SaveChangesAsync();

        return new CreateManyProjectMembersResult
        {
            ProjectMembers = createdMembers
        };
    }

    public async Task<ProjectMemberDto> UpdateAsync(UpdateProjectMemberParameters parameters)
    {
        var member = await dbContext.ProjectMembers
            .FirstOrDefaultAsync(x => x.ProjectId == parameters.ProjectId && x.UserId == parameters.UserId);

        if (member == null)
        {
            throw new ProjectMemberNotFoundException(parameters.ProjectId, parameters.UserId);
        }

        if (member.Role != (DataAccess.Enums.ProjectMemberRole)parameters.Role)
        {
            member.Role = (DataAccess.Enums.ProjectMemberRole)parameters.Role;

            dbContext.ProjectMembers.Update(member);
            await dbContext.SaveChangesAsync();
        }

        return member.ToDto();
    }

    public async Task DeleteAsync(DeleteProjectMemberParameters parameters)
    {
        var member = await dbContext.ProjectMembers
            .FirstOrDefaultAsync(x => x.ProjectId == parameters.ProjectId && x.UserId == parameters.UserId);

        if (member == null)
        {
            throw new ProjectMemberNotFoundException(parameters.ProjectId, parameters.UserId);
        }

        dbContext.ProjectMembers.Remove(member);
        await dbContext.SaveChangesAsync();
    }
}