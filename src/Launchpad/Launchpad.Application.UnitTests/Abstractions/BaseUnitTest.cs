using Launchpad.Application.Tests.Fixtures;
using Launchpad.Persistence;
using Launchpad.Shared;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Abstractions;

[Trait("Category", "Unit")]
public abstract class BaseUnitTest : IDisposable
{
    private readonly SqliteConnection _connection;
    protected readonly ApplicationDbContext DbContext;

    protected readonly JwtDescriptorDetails DefaultJwtDetails = new JwtDescriptorDetails
    {
        Key = "super_secret_key_that_is_long_enough_for_sha256",
        Audience = "test-audience",
        Issuer = "test-issuer",
        TokenLifetimeInHours = 1
    };
    protected readonly Fixture Fixture = new Fixture();

    protected BaseUnitTest()
    {
        _connection = new SqliteConnection("DataSource=:memory:;Foreign Keys=False");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        DbContext = new ApplicationDbContext(options);
        DbContext.Database.EnsureCreated();

        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        Fixture.Customize(new EmployeeFixture());
        Fixture.Customize(new EducationLevelFixture());
        Fixture.Customize(new EmployeeEducationFixture());
        Fixture.Customize(new SkillFixture());
        Fixture.Customize(new EmployeeProjectFixture());
        Fixture.Customize(new EmployerFixture());
        Fixture.Customize(new DateOnlyFixture());
        Fixture.Customize(new ActivityFieldGroupFixture());
        Fixture.Customize(new ActivityFieldFixture());
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}