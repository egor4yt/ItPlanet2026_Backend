FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Launchpad.Candidates.Api/Launchpad.Candidates.Api.csproj", "Launchpad.Candidates.Api/"]
COPY ["Launchpad.Candidates.Shared/Launchpad.Candidates.Shared.csproj", "Launchpad.Candidates.Shared/"]
COPY ["Launchpad.Candidates.Application/Launchpad.Candidates.Application.csproj", "Launchpad.Candidates.Application/"]
COPY ["Launchpad.Candidates.Infrastructure/Launchpad.Candidates.Infrastructure.csproj", "Launchpad.Candidates.Infrastructure/"]
COPY ["Launchpad.Candidates.Domain/Launchpad.Candidates.Domain.csproj", "Launchpad.Candidates.Domain/"]
RUN dotnet restore "Launchpad.Candidates.Api/Launchpad.Candidates.Api.csproj"
COPY . .
WORKDIR "/src/Launchpad.Candidates.Api"
RUN dotnet build "./Launchpad.Candidates.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Launchpad.Candidates.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Launchpad.Candidates.Api.dll"]
