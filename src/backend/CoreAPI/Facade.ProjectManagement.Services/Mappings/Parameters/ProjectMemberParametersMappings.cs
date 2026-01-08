namespace Facade.ProjectManagement.Services.Mappings.Parameters;

public static class ProjectMemberParametersMappings
{
    public static Project.Contracts.Parameters.ProjectMember.GetManyProjectMembersByProjectIdParameters ToCoreParameters(this Facade.ProjectManagement.Contracts.Parameters.ProjectMember.GetManyProjectMembersByProjectIdParameters parameters)
    {
        return new Project.Contracts.Parameters.ProjectMember.GetManyProjectMembersByProjectIdParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
    
    public static Project.Contracts.Parameters.ProjectMember.GetProjectMemberByIdParameters ToCoreParameters(this Facade.ProjectManagement.Contracts.Parameters.ProjectMember.GetProjectMemberByIdParameters parameters)
    {
        return new Project.Contracts.Parameters.ProjectMember.GetProjectMemberByIdParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            UserId = parameters.MemberUserId
        };
    }
    
    public static Project.Contracts.Parameters.ProjectMember.CreateProjectMemberParameters ToCoreParameters(this Facade.ProjectManagement.Contracts.Parameters.ProjectMember.CreateProjectMemberParameters parameters)
    {
        return new Project.Contracts.Parameters.ProjectMember.CreateProjectMemberParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            UserId = parameters.MemberUserId,
            Role = (Project.Contracts.Dtos.ProjectMemberRole)parameters.Role
        };
    }
    
    public static Project.Contracts.Parameters.ProjectMember.UpdateProjectMemberParameters ToCoreParameters(this Facade.ProjectManagement.Contracts.Parameters.ProjectMember.UpdateProjectMemberParameters parameters)
    {
        return new Project.Contracts.Parameters.ProjectMember.UpdateProjectMemberParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            UserId = parameters.MemberUserId,
            Role = (Project.Contracts.Dtos.ProjectMemberRole)parameters.Role
        };
    }
    
    public static Project.Contracts.Parameters.ProjectMember.DeleteProjectMemberParameters ToCoreParameters(this Facade.ProjectManagement.Contracts.Parameters.ProjectMember.DeleteProjectMemberParameters parameters)
    {
        return new Project.Contracts.Parameters.ProjectMember.DeleteProjectMemberParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            UserId = parameters.MemberUserId
        };
    }
}