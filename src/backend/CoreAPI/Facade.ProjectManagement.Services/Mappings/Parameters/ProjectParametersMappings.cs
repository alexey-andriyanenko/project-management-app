namespace Facade.ProjectManagement.Services.Mappings.Parameters;

public static class ProjectParametersMappings
{
    public static Project.Contracts.Parameters.Project.GetManyProjectsByTenantIdParameters ToCoreParameters(this Contracts.Parameters.Project.GetManyProjectsByTenantIdParameters parameters)
    {
        return new Project.Contracts.Parameters.Project.GetManyProjectsByTenantIdParameters
        {
            TenantId = parameters.TenantId
        };
    }
    
    public static Project.Contracts.Parameters.Project.GetProjectByIdParameters ToCoreParameters(this Contracts.Parameters.Project.GetProjectByIdParameters parameters)
    {
        return new Project.Contracts.Parameters.Project.GetProjectByIdParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
    
    public static Project.Contracts.Parameters.Project.CreateProjectParameters ToCoreParameters(this Contracts.Parameters.Project.CreateProjectParameters parameters)
    {
        return new Project.Contracts.Parameters.Project.CreateProjectParameters
        {
            TenantId = parameters.TenantId,
            Name = parameters.Name,
            Description = parameters.Description,
            Members = parameters.Members.Select(m => m.ToCoreParameters()).ToList()
        };
    }
    
    private static Project.Contracts.Parameters.Project.CreateProjectMemberItemParameters ToCoreParameters(this Contracts.Parameters.Project.CreateProjectMemberItemParameters parameters)
    {
        return new Project.Contracts.Parameters.Project.CreateProjectMemberItemParameters
        {
            UserId = parameters.UserId,
            Role = (Project.Contracts.Dtos.ProjectMemberRole)parameters.Role
        };
    }
    
    public static Project.Contracts.Parameters.Project.UpdateProjectParameters ToCoreParameters(this Contracts.Parameters.Project.UpdateProjectParameters parameters)
    {
        return new Project.Contracts.Parameters.Project.UpdateProjectParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            Name = parameters.Name,
            Description = parameters.Description,
        };
    }
    
    public static Project.Contracts.Parameters.Project.DeleteProjectParameters ToCoreParameters(this Contracts.Parameters.Project.DeleteProjectParameters parameters)
    {
        return new Project.Contracts.Parameters.Project.DeleteProjectParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
}