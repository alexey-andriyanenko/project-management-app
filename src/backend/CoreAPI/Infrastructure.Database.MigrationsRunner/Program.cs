using Board.DataAccess.DI;
using Identity.DataAccess.DI;
using Infrastructure.Database.MigrationsRunner.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.DataAccess.DI;
using Tag.DataAccess.DI;
using Tenant.DataAccess.DI;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

if (builder.Environment.IsProduction())
{
    var systemManagerPath = Environment.GetEnvironmentVariable("AWS_SYSTEM_MANAGER_PATH") ??
                            throw new ArgumentNullException("AWS_SYSTEM_MANAGER_PATH environment variable is not set");

    builder.Configuration.AddSystemsManager(systemManagerPath);
}

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddIdentityDataAccess(builder.Configuration);
builder.Services.AddTenantDataAccess(builder.Configuration);
builder.Services.AddProjectDataAccess(builder.Configuration);
builder.Services.AddBoardDataAccess(builder.Configuration);
builder.Services.AddTagDataAccess(builder.Configuration);

var host = builder.Build();

using var scope = host.Services.CreateScope();

var cancellationToken = host.Services
    .GetRequiredService<IHostApplicationLifetime>()
    .ApplicationStopping;
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
var migrationsRunners = scope.ServiceProvider.GetServices<IModuleMigrationsRunner>();

foreach (var migrationsRunner in migrationsRunners)
{
    logger.LogInformation("Running migrations for module: {ModuleName}", migrationsRunner.ModuleName);
    await migrationsRunner.RunMigrationsAsync(cancellationToken);
    logger.LogInformation("Migrations completed for module: {ModuleName}", migrationsRunner.ModuleName);
}

logger.LogInformation("All migrations completed successfully.");
