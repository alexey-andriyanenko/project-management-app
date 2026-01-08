namespace Tag.Contracts.Parameters;

public class DeleteTagParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid? ProjectId { get; set; }
}