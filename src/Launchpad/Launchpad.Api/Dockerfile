FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Launchpad.Api/Launchpad.Api.csproj", "Launchpad.Api/"]
COPY ["Launchpad.Application/Launchpad.Application.csproj", "Launchpad.Application/"]
COPY ["Launchpad.Persistence/Launchpad.Persistence.csproj", "Launchpad.Persistence/"]
COPY ["Launchpad.Domain/Launchpad.Domain.csproj", "Launchpad.Domain/"]
COPY ["Launchpad.Shared/Launchpad.Shared.csproj", "Launchpad.Shared/"]
RUN dotnet restore "Launchpad.Api/Launchpad.Api.csproj"
COPY . .
WORKDIR "/src/Launchpad.Api"
RUN dotnet build "./Launchpad.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Launchpad.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Launchpad.Api.dll"]
