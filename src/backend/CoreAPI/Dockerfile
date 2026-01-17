FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Facade.API/Facade.API.csproj", "Facade.API/"]
COPY ["Tenant.Services/Tenant.Services.csproj", "Tenant.Services/"]
COPY ["Shared.String.Extensions/Shared.String.Extensions.csproj", "Shared.String.Extensions/"]
COPY ["Tenant.Contracts/Tenant.Contracts.csproj", "Tenant.Contracts/"]
COPY ["Tenant.DataAccess/Tenant.DataAccess.csproj", "Tenant.DataAccess/"]
COPY ["Shared.EntityFrameworkCore.Configuration/Shared.EntityFrameworkCore.Configuration.csproj", "Shared.EntityFrameworkCore.Configuration/"]
COPY ["Tenant.Events.Contracts/Tenant.Events.Contracts.csproj", "Tenant.Events.Contracts/"]
COPY ["Infrastructure.EventBus.Contracts/Infrastructure.EventBus.Contracts.csproj", "Infrastructure.EventBus.Contracts/"]
COPY ["Tenant.DI/Tenant.DI.csproj", "Tenant.DI/"]
COPY ["Tenant.Client/Tenant.Client.csproj", "Tenant.Client/"]
COPY ["Tenant.Client.Contracts/Tenant.Client.Contracts.csproj", "Tenant.Client.Contracts/"]
COPY ["Tag.Services/Tag.Services.csproj", "Tag.Services/"]
COPY ["Tag.Contracts/Tag.Contracts.csproj", "Tag.Contracts/"]
COPY ["Tag.DataAccess/Tag.DataAccess.csproj", "Tag.DataAccess/"]
COPY ["Tag.Events/Tag.Events.csproj", "Tag.Events/"]
COPY ["Project.Events.Contracts/Project.Events.Contracts.csproj", "Project.Events.Contracts/"]
COPY ["Tag.DI/Tag.DI.csproj", "Tag.DI/"]
COPY ["Tag.Client/Tag.Client.csproj", "Tag.Client/"]
COPY ["Tag.Client.Contracts/Tag.Client.Contracts.csproj", "Tag.Client.Contracts/"]
COPY ["Project.Services/Project.Services.csproj", "Project.Services/"]
COPY ["Project.Contracts/Project.Contracts.csproj", "Project.Contracts/"]
COPY ["Project.DataAccess/Project.DataAccess.csproj", "Project.DataAccess/"]
COPY ["Project.Events/Project.Events.csproj", "Project.Events/"]
COPY ["Project.DI/Project.DI.csproj", "Project.DI/"]
COPY ["Project.Client/Project.Client.csproj", "Project.Client/"]
COPY ["Project.Client.Contracts/Project.Client.Contracts.csproj", "Project.Client.Contracts/"]
COPY ["Identity.Services/Identity.Services.csproj", "Identity.Services/"]
COPY ["Identity.Contracts/Identity.Contracts.csproj", "Identity.Contracts/"]
COPY ["Identity.DataAccess/Identity.DataAccess.csproj", "Identity.DataAccess/"]
COPY ["Identity.DI/Identity.DI.csproj", "Identity.DI/"]
COPY ["Identity.Client/Identity.Client.csproj", "Identity.Client/"]
COPY ["Identity.Client.Contracts/Identity.Client.Contracts.csproj", "Identity.Client.Contracts/"]
COPY ["Facade.TenantManagement.Services/Facade.TenantManagement.Services.csproj", "Facade.TenantManagement.Services/"]
COPY ["Facade.TenantManagement.Contracts/Facade.TenantManagement.Contracts.csproj", "Facade.TenantManagement.Contracts/"]
COPY ["Facade.TenantManagement.Controllers/Facade.TenantManagement.Controllers.csproj", "Facade.TenantManagement.Controllers/"]
COPY ["Facade.TagManagement.Services/Facade.TagManagement.Services.csproj", "Facade.TagManagement.Services/"]
COPY ["Facade.TagManagement.Contracts/Facade.TagManagement.Contracts.csproj", "Facade.TagManagement.Contracts/"]
COPY ["Facade.TagManagement.Controllers/Facade.TagManagement.Controllers.csproj", "Facade.TagManagement.Controllers/"]
COPY ["Facade.ProjectManagement.Services/Facade.ProjectManagement.Services.csproj", "Facade.ProjectManagement.Services/"]
COPY ["Facade.ProjectManagement.Contracts/Facade.ProjectManagement.Contracts.csproj", "Facade.ProjectManagement.Contracts/"]
COPY ["Facade.ProjectManagement.Controllers/Facade.ProjectManagement.Controllers.csproj", "Facade.ProjectManagement.Controllers/"]
COPY ["Facade.IdentityManagement.Services/Facade.IdentityManagement.Services.csproj", "Facade.IdentityManagement.Services/"]
COPY ["Facade.IdentityManagement.Contracts/Facade.IdentityManagement.Contracts.csproj", "Facade.IdentityManagement.Contracts/"]
COPY ["Facade.IdentityManagement.Controllers/Facade.IdentityManagement.Controllers.csproj", "Facade.IdentityManagement.Controllers/"]
COPY ["Facade.BoardManagement.Services/Facade.BoardManagement.Services.csproj", "Facade.BoardManagement.Services/"]
COPY ["Board.Client.Contracts/Board.Client.Contracts.csproj", "Board.Client.Contracts/"]
COPY ["Board.Contracts/Board.Contracts.csproj", "Board.Contracts/"]
COPY ["Facade.BoardManagement.Contracts/Facade.BoardManagement.Contracts.csproj", "Facade.BoardManagement.Contracts/"]
COPY ["Facade.BoardManagement.Controllers/Facade.BoardManagement.Controllers.csproj", "Facade.BoardManagement.Controllers/"]
COPY ["Board.Services/Board.Services.csproj", "Board.Services/"]
COPY ["Board.DataAccess/Board.DataAccess.csproj", "Board.DataAccess/"]
COPY ["Board.Events.Contracts/Board.Events.Contracts.csproj", "Board.Events.Contracts/"]
COPY ["Board.Events/Board.Events.csproj", "Board.Events/"]
COPY ["Board.DI/Board.DI.csproj", "Board.DI/"]
COPY ["Board.Client/Board.Client.csproj", "Board.Client/"]
COPY ["Infrastructure.EventBus.InMemory/Infrastructure.EventBus.InMemory.csproj", "Infrastructure.EventBus.InMemory/"]
COPY ["Facade.TenantManagement.DI/Facade.TenantManagement.DI.csproj", "Facade.TenantManagement.DI/"]
COPY ["Facade.TagManagement.DI/Facade.TagManagement.DI.csproj", "Facade.TagManagement.DI/"]
COPY ["Facade.ProjectManagement.DI/Facade.ProjectManagement.DI.csproj", "Facade.ProjectManagement.DI/"]
COPY ["Facade.IdentityManagement.DI/Facade.IdentityManagement.DI.csproj", "Facade.IdentityManagement.DI/"]
COPY ["Facade.BoardManagement.DI/Facade.BoardManagement.DI.csproj", "Facade.BoardManagement.DI/"]
RUN dotnet restore "Facade.API/Facade.API.csproj"
COPY . .
WORKDIR "/src/Facade.API"
RUN dotnet build "./Facade.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Facade.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Facade.API.dll"]
