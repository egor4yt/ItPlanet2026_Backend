using System.Data.Common;
using Launchpad.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace Launchpad.Application.IntegrationTests.Abstractions;

[CollectionDefinition("ApiCollection")]
public class ApiCollection : ICollectionFixture<ApiWebApplicationFactory>
{
}

public class ApiWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:15-alpine").Build();

    public Respawner Respawner { get; private set; } = null!;
    public DbConnection DbConnection { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

        await DbConnection.OpenAsync();

        Respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
    }


    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ConfigurationKeys.SqlDatabaseConnectionString] = _dbContainer.GetConnectionString()
            });
        });

        builder.ConfigureServices(services => { });
    }

    public async Task ResetDatabaseAsync()
    {
        await Respawner.ResetAsync(DbConnection);
    }
}