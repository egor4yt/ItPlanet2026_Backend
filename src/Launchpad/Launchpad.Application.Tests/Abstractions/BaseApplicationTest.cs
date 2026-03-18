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

    protected readonly JwtDescriptorDetails DefaultJwtDetails = new()
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
        
        
        Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => Fixture.Behaviors.Remove(b));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        Fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

        Fixture.Customize<Employee>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeEducations)
            .Without(x => x.EmployeeProjects)
            .Without(x => x.Skills));
        
        Fixture.Customize<EducationLevel>(composer => composer
            .Without(x => x.EmployeeEducations)
            .With(x => x.Title, () => "Level " + Guid.NewGuid()));
        
        Fixture.Customize<EmployeeEducation>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeId)
            .Without(x => x.EducationLevelId)
            .Without(x => x.Employee)
            .Without(x => x.EducationLevel));
        
        Fixture.Customize<Skill>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.Employees));
        
        Fixture.Customize<EmployeeProject>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeId)
            .Without(x => x.Employee));
        
        Fixture.Customize<Employer>(composer => composer
            .Without(x => x.Id));
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Close();
        _connection.Dispose();
    }
}
