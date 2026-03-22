using System.Data.Common;
using Launchpad.Persistence;
using Launchpad.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
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
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ConfigurationKeys.SqlDatabaseConnectionString] = _dbContainer.GetConnectionString()
            });
        });

        builder.ConfigureServices(services => 
        {
            // Находим старую регистрацию DbContextOptions и удаляем её
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Регистрируем заново с отключенным ворнингом
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
                options.ConfigureWarnings(warnings => 
                    warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            });
        });
    }

    public async Task ResetDatabaseAsync()
    {
    }
}