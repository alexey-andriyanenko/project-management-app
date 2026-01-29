namespace Facade.TagManagement.Services.Mappings;

public static class ParametersMappings
{
    public static Tag.Contracts.Parameters.GetManyTagsByTenantIdParameters ToCoreParameters(this Facade.TagManagement.Contracts.Parameters.GetManyTagsByTenantIdParameters parameters)
    {
        return new Tag.Contracts.Parameters.GetManyTagsByTenantIdParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
    
    public static Tag.Contracts.Parameters.GetManyTagsByIdsParameters ToCoreParameters(this Facade.TagManagement.Contracts.Parameters.GetManyTagsByIdsParameters parameters)
    {
        return new Tag.Contracts.Parameters.GetManyTagsByIdsParameters
        {
            TenantId = parameters.TenantId,
            TagIds = parameters.TagIds
        };
    }
    
    public static Tag.Contracts.Parameters.CreateTagParameters ToCoreParameters(this Facade.TagManagement.Contracts.Parameters.CreateTagParameters parameters)
    {
        return new Tag.Contracts.Parameters.CreateTagParameters
        {
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            Name = parameters.Name,
            Color = parameters.Color
        };
    }
    
    public static Tag.Contracts.Parameters.UpdateTagParameters ToCoreParameters(this Facade.TagManagement.Contracts.Parameters.UpdateTagParameters parameters)
    {
        return new Tag.Contracts.Parameters.UpdateTagParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            Name = parameters.Name,
            Color = parameters.Color
        };
    }
    
    public static Tag.Contracts.Parameters.DeleteTagParameters ToCoreParameters(this Facade.TagManagement.Contracts.Parameters.DeleteTagParameters parameters)
    {
        return new Tag.Contracts.Parameters.DeleteTagParameters
        {
            Id = parameters.Id,
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId
        };
    }
}