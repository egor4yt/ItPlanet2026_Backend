using AutoFixture;
using Launchpad.Domain.Entities;
using Launchpad.Persistence;
using Launchpad.Shared;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Abstractions;

public abstract class BaseApplicationTest : IDisposable
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly Fixture Fixture = new Fixture();
    private readonly SqliteConnection _connection;

    protected readonly JwtDescriptorDetails DefaultJwtDetails = new JwtDescriptorDetails
    {
        Key = "super_secret_key_that_is_long_enough_for_sha256",
        Audience = "test-audience",
        Issuer = "test-issuer",
        TokenLifetimeInHours = 1
    };

    protected BaseApplicationTest()
    {
        _connection = new SqliteConnection("DataSource=:memory:;Foreign Keys=False");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .EnableSensitiveDataLogging()
            .Options;

        DbContext = new ApplicationDbContext(options);
        DbContext.Database.EnsureCreated();
        
        Fixture.Customize(new FixtureCustomization());
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}
