namespace Facade.TenantManagement.Services.Mappings;

public static class ParametersMappings
{
    public static Tenant.Contracts.Parameters.GetTenantBySlugParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.GetTenantBySlugParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.GetTenantBySlugParameters
        {
            MemberId = parameters.MemberId,
            Slug = parameters.Slug,
        };
    }
    
    public static Tenant.Contracts.Parameters.GetManyTenantsByUserIdParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.GetManyTenantsByUserIdParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.GetManyTenantsByUserIdParameters
        {
            UserId = parameters.UserId,
        };
    }
    
    public static Tenant.Contracts.Parameters.CreateTenantParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.CreateTenantParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.CreateTenantParameters
        {
            UserId = parameters.UserId,
            Name = parameters.Name,
        };
    }
    
    public static Tenant.Contracts.Parameters.UpdateTenantParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.UpdateTenantParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.UpdateTenantParameters
        {
            Id = parameters.TenantId,
            Name = parameters.Name,
        };
    }
    
    public static Tenant.Contracts.Parameters.GetManyTenantMembersByTenantIdParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.GetManyTenantMembersByTenantIdParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.GetManyTenantMembersByTenantIdParameters
        {
            TenantId = parameters.TenantId,
        };
    }
    
    public static Tenant.Contracts.Parameters.CreateTenantMemberParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.CreateTenantMemberParameters parameters)
    {
        return new Tenant.Contracts.Parameters.CreateTenantMemberParameters
        {
            TenantId = parameters.TenantId,
            Role = (Tenant.Contracts.Dtos.TenantMemberRole)parameters.Role
        };
    }
    
    public static Tenant.Contracts.Parameters.UpdateTenantMemberParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.UpdateTenantMemberParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.UpdateTenantMemberParameters
        {
            TenantId = parameters.TenantId,
            UserId = parameters.MemberUserId,
            Role = (Tenant.Contracts.Dtos.TenantMemberRole)parameters.Role,
        };
    }
    
    public static Tenant.Contracts.Parameters.DeleteTenantMemberParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.DeleteTenantMemberParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.DeleteTenantMemberParameters
        {
            TenantId = parameters.TenantId,
            UserId = parameters.MemberUserId,
        };
    }
    
    public static Tenant.Contracts.Parameters.DeleteTenantParameters ToCoreParameters(this Facade.TenantManagement.Contracts.Parameters.DeleteTenantParameters parameters) 
    {
        return new Tenant.Contracts.Parameters.DeleteTenantParameters
        {
            TenantId = parameters.TenantId,
        };
    }
}