using Facade.BoardManagement.DI;
using Facade.IdentityManagement.DI;
using Facade.ProjectManagement.DI;
using Facade.TagManagement.DI;
using Facade.TenantManagement.DI;
using Infrastructure.EventBus.InMemory.DI;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(x =>
        x.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

builder.Services.AddInMemoryEventBus();

builder.Services
    .AddFacadeIdentityManagementModule(configuration)
    .AddFacadeTenantManagementModule(configuration)
    .AddFacadeProjectManagementModule(configuration)
    .AddFacadeTagManagementModule(configuration)
    .AddFacadeBoardManagementModule(configuration);

builder.Services
    .AddControllers()
    .AddFacadeIdentityManagementControllers()
    .AddFacadeTenantManagementControllers()
    .AddFacadeProjectManagementControllers()
    .AddFacadeTagManagementControllers()
    .AddFacadeBoardManagementControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Facade.API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Facade.API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();