using System.Text;
using Facade.BoardManagement.DI;
using Facade.IdentityManagement.DI;
using Facade.ProjectManagement.DI;
using Facade.TagManagement.DI;
using Facade.TenantManagement.DI;
using Infrastructure.EventBus.InMemory.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    var systemManagerPath = Environment.GetEnvironmentVariable("AWS_SYSTEM_MANAGER_PATH") ??
                     throw new ArgumentNullException("AWS_SYSTEM_MANAGER_PATH environment variable is not set");

    builder.Configuration.AddSystemsManager(systemManagerPath);
}

var configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var jwtSettings = configuration.GetSection("JwtSettings");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),

            ValidateLifetime = true,
        }; 
    });

builder.Services.AddAuthorization();

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
    .AddFacadeBoardManagementControllers()
    .AddMvcOptions(options =>
    {
        var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
        // Allow anonymous access to Swagger endpoints
        // options.Filters.Add(new AllowAnonymousFilterForSwagger());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Facade.API v1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");

app.MapControllers();

await app.RunAsync();
