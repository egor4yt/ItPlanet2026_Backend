using System.Net.Http.Headers;
using AutoFixture;
using Launchpad.Api.Configuration.Options;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using Launchpad.Tests.Base.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Launchpad.Application.IntegrationTests.Abstractions;

[Collection("ApiCollection")]
[Trait("Category", "Integration")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly ApiWebApplicationFactory _factory;
    protected readonly IFixture Fixture = new Fixture();
    private IServiceScope _scope = null!;

    protected BaseIntegrationTest(ApiWebApplicationFactory factory)
    {
        _factory = factory;

        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture.RegisterAllFixtureCustomizations();
    }

    protected ApplicationDbContext ApplicationDbContext { get; private set; } = null!;

    protected HttpClient HttpClient { get; private set; } = null!;

    public virtual async Task InitializeAsync()
    {
        _scope = _factory.Services.CreateScope();
        ApplicationDbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        HttpClient = _factory.CreateClient();
        await _factory.ResetDatabaseAsync();
    }

    public virtual Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }

    protected void Authenticate(Employee employee)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "employee");
    }

    protected void Authenticate(Employer employer)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "employee");
    }

    protected void Authenticate(Curator curator)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "curator");
    }
}