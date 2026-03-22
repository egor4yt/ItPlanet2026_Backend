using AutoFixture;
using Launchpad.Persistence;
using Launchpad.Tests.Base.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Launchpad.Application.IntegrationTests.Abstractions;

[Collection("ApiCollection")]
[Trait("Category", "Integration")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly Func<Task> _resetDb;
    private readonly IServiceScope _scope;
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly IFixture Fixture = new Fixture();
    protected readonly HttpClient HttpClient;

    protected BaseIntegrationTest(ApiWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();
        ApplicationDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        HttpClient = factory.CreateClient();


        var migrations = ApplicationDbContext.Database.GetPendingMigrations().ToList();
        Console.WriteLine(string.Join(Environment.NewLine, migrations));
        if (migrations.Count != 0) ApplicationDbContext.Database.Migrate();

        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture.RegisterAllFixtureCustomizations();

        _resetDb = factory.ResetDatabaseAsync;
    }

    public virtual Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public virtual async Task DisposeAsync()
    {
        _scope.Dispose();
        await _resetDb();
    }
}