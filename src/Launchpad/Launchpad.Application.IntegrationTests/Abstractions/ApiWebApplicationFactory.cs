using System.Data.Common;
using Launchpad.Persistence;
using Launchpad.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace Launchpad.Application.IntegrationTests.Abstractions;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgis/postgis:15-3.5-alpine").Build();

    public Respawner Respawner { get; private set; } = null!;
    public DbConnection DbConnection { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        _ = Services; // trigger Services for start migrations

        DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

        await DbConnection.OpenAsync();

        Respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore =
            [
                "__EFMigrationsHistory",
                nameof(ApplicationDbContext.EducationLevels),
                nameof(ApplicationDbContext.EmployerVerificationStatuses),
                nameof(ApplicationDbContext.ActivityFields),
                nameof(ApplicationDbContext.ActivityFieldGroups),
                nameof(ApplicationDbContext.EmployerVerificationTypes)
            ]
        });
    }


    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        DbConnection.Dispose();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting(ConfigurationKeys.SqlDatabaseConnectionString, _dbContainer.GetConnectionString());
        builder.UseSetting(ConfigurationKeys.Environment, Environments.IntegrationTests);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ConfigurationKeys.SqlDatabaseConnectionString] = _dbContainer.GetConnectionString(),
                [ConfigurationKeys.Environment] = Environments.IntegrationTests
            });
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await Respawner.ResetAsync(DbConnection);
    }
}