namespace Infrastructure.Database.MigrationsRunner.Contracts;

public interface IModuleMigrationsRunner
{
    public string ModuleName { get; }
    
    public Task RunMigrationsAsync(CancellationToken cancellationToken = default);
}
