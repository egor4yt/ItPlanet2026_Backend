using System.Data.Common;
using Launchpad.Persistence;
using Launchpad.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString());

        await using (var context = new ApplicationDbContext(optionsBuilder.Options))
        {
            var hasMigrations = (await context.Database.GetPendingMigrationsAsync()).ToList();
            if (hasMigrations.Count > 0) await context.Database.MigrateAsync();
        }

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