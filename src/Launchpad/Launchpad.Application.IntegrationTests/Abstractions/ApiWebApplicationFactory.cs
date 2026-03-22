using System.Data.Common;
using Launchpad.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Launchpad.Application.IntegrationTests.Abstractions;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:15-alpine").Build();

    public DbConnection DbConnection { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

        await DbConnection.OpenAsync();
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
    }

    public async Task ResetDatabaseAsync()
    {
    }
}