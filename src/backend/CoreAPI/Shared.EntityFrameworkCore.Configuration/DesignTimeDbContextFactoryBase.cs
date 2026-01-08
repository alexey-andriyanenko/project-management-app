using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Shared.EntityFrameworkCore.Configuration
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        public TContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
            
            var connName = GetConnectionStringName();
            var conn = config.GetConnectionString(connName)
                       ?? throw new InvalidOperationException($"Connection string '{connName}' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            ConfigureOptions(optionsBuilder, conn);

            return CreateNewInstance(optionsBuilder.Options);
        }

        protected virtual void ConfigureOptions(DbContextOptionsBuilder<TContext> builder, string connectionString)
        {
            throw new NotImplementedException("You must implement ConfigureOptions to configure the DbContext options.");
        }

        protected abstract string GetConnectionStringName();
    }
}