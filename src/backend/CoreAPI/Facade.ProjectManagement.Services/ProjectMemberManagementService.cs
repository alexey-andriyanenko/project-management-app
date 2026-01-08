using Facade.ProjectManagement.Contracts.Dtos;
using Facade.ProjectManagement.Contracts.Parameters.ProjectMember;
using Facade.ProjectManagement.Contracts.Results;
using Facade.ProjectManagement.Contracts.Services;
using Facade.ProjectManagement.Services.Mappings.Dtos;
using Facade.ProjectManagement.Services.Mappings.Parameters;
using Identity.Contracts.Parameters.User;

namespace Facade.ProjectManagement.Services;

public class ProjectMemberManagementService(
    Project.Client.Contracts.IProjectClient projectClient,
    Identity.Client.Contracts.IIdentityClient identityClient) : IProjectMemberManagementService
{
    public async Task<GetManyProjectMembersByProjectIdResult> GetManyAsync(
        GetManyProjectMembersByProjectIdParameters parameters)
    {
        var result = await projectClient.ProjectMemberResource.GetManyAsync(parameters.ToCoreParameters());
        var enrichedMembers = await EnrichProjectMembers(result.ProjectMembers);
        
        return new GetManyProjectMembersByProjectIdResult
        {
            ProjectMembers = enrichedMembers
        };
    }

    public async Task<ProjectMemberDto> GetAsync(GetProjectMemberByIdParameters parameters)
    {
        try
        {
            var coreMember = await projectClient.ProjectMemberResource.GetAsync(parameters.ToCoreParameters());
            var enrichedMembers = await EnrichProjectMembers([coreMember]);
            return enrichedMembers.First();
        }
        catch (Project.Contracts.Exceptions.ProjectMemberNotFoundException)
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectMemberNotFoundException(parameters.ProjectId, parameters.MemberUserId);
        }
    }
    
    public async Task<ProjectMemberDto> CreateAsync(CreateProjectMemberParameters parameters)
    {
        try
        {
            var coreMember = await projectClient.ProjectMemberResource.CreateAsync(parameters.ToCoreParameters());
            var enrichedMembers = await EnrichProjectMembers([coreMember]);
            return enrichedMembers.First();
        }
        catch (Project.Contracts.Exceptions.ProjectNotFoundException)
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectNotFoundException(parameters.ProjectId);
        }
    }
    
    public async Task<ProjectMemberDto> UpdateAsync(UpdateProjectMemberParameters parameters)
    {
        try
        {
            var coreMember = await projectClient.ProjectMemberResource.UpdateAsync(parameters.ToCoreParameters());
            var enrichedMembers = await EnrichProjectMembers([coreMember]);
            return enrichedMembers.First();
        }
        catch (Project.Contracts.Exceptions.ProjectMemberNotFoundException)
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectMemberNotFoundException(parameters.ProjectId, parameters.MemberUserId);
        }
    }
    
    public async Task DeleteAsync(DeleteProjectMemberParameters parameters)
    {
        try
        {
            await projectClient.ProjectMemberResource.DeleteAsync(parameters.ToCoreParameters());
        }
        catch (Project.Contracts.Exceptions.ProjectMemberNotFoundException)
        {
            throw new Facade.ProjectManagement.Contracts.Exceptions.ProjectMemberNotFoundException(parameters.ProjectId, parameters.MemberUserId);
        }
    }
    
    private async Task<IReadOnlyList<ProjectMemberDto>> EnrichProjectMembers(
        IReadOnlyList<Project.Contracts.Dtos.ProjectMemberDto> coreMembers)
    {
        var userIds = coreMembers.Select(pm => pm.UserId).Distinct().ToList();
        var usersResult = await identityClient.UserResource.GetManyAsync(new GetManyUsersByIdsParameters()
        {
            Ids = userIds
        });
        var usersById = usersResult.Users.ToDictionary(u => u.Id, u => u);

        return coreMembers
            .Select(pm =>
            {
                var user = usersById[pm.UserId];
                return pm.ToFacadeDto(user);
            }).ToList();
    }
}