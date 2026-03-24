using Launchpad.Persistence;
using Launchpad.Shared;
using Launchpad.Tests.Base.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Launchpad.Application.Tests.Abstractions;

[Trait("Category", "Unit")]
[Collection("PostgisCollection")]
public abstract class BaseUnitTest : IDisposable
{
    protected readonly ApplicationDbContext DbContext;

    protected readonly JwtDescriptorDetails DefaultJwtDetails = new JwtDescriptorDetails
    {
        Key = "super_secret_key_that_is_long_enough_for_sha256",
        Audience = "test-audience",
        Issuer = "test-issuer",
        TokenLifetimeInHours = 1
    };
    protected readonly Fixture Fixture = new Fixture();

    protected BaseUnitTest(PostgisFixture postgis)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(postgis.ConnectionString + ";Include Error Detail=true", x => x.UseNetTopologySuite())
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            .Options;

        DbContext = new ApplicationDbContext(options);
        DbContext.Database.EnsureCreated();

        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Fixture.RegisterAllFixtureCustomizations();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}