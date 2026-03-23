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

    protected void Authenticate(Employee employee)
    {
        var jwtOptions = _scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();
        var jwtDetails = new JwtDetails(employee);
        var token = SecurityHelper.GenerateJwtToken(jwtOptions.Value.ToJwtDescriptorDetails(), jwtDetails);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    protected void Authenticate(Employer employer)
    {
        var jwtOptions = _scope.ServiceProvider.GetRequiredService<IOptions<JwtOptions>>();
        var jwtDetails = new JwtDetails(employer);
        var token = SecurityHelper.GenerateJwtToken(jwtOptions.Value.ToJwtDescriptorDetails(), jwtDetails);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}