using Testcontainers.PostgreSql;

namespace Launchpad.Application.Tests.Abstractions;

public class PostgisFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder("postgis/postgis:15-3.5-alpine").Build();

    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}