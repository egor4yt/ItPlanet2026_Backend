using Launchpad.Application.Commands.Employers.Update;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employers.Update;

public class UpdateEmployersCommandHandlerTests : BaseApplicationTest
{
    private readonly UpdateEmployersCommandHandler _handler;

    public UpdateEmployersCommandHandlerTests()
    {
        _handler = new UpdateEmployersCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldUpdateEmployer_WhenRequestIsValid()
    {
        // Arrange
       var activityFieldGroup = Fixture.Create<ActivityFieldGroup>();
        await DbContext.ActivityFieldGroups.AddAsync(activityFieldGroup);
        
        var activityField = Fixture.Create<ActivityField>();
        var otherActivityField = Fixture.Create<ActivityField>();
        await DbContext.ActivityFields.AddAsync(activityField);
        await DbContext.ActivityFields.AddAsync(otherActivityField);
        
        var employer = Fixture
            .Build<Employer>()
            .With(x => x.ActivityFields, [activityField])
            .Create();
        await DbContext.Employers.AddAsync(employer);
        
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateEmployersCommandRequest>()
            .With(x => x.EmployerId, employer.Id)
            .With(x => x.ActivityFieldIds, [otherActivityField.Id])
            .Create();

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var employerInDb = await DbContext.Employers
            .Include(x => x.ActivityFields)
            .FirstOrDefaultAsync(x => x.Id == employer.Id);

        employerInDb.Should().NotBeNull();
        employerInDb.Description.Should().Be(request.Description);
        employerInDb.CompanyName.Should().Be(employer.CompanyName);
        employerInDb.RegisteredOn.Should().Be(employer.RegisteredOn);
        employerInDb.ActivityFields.Select(x => x.Id).Should().HaveCount(1);
        employerInDb.ActivityFields.Select(x => x.Id).Should().Contain(request.ActivityFieldIds);
    }

    [Fact]
    public async Task Handle_ShouldUpdateEmployer_WhenDescriptionIsEmpty()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        await DbContext.Employers.AddAsync(employer);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<UpdateEmployersCommandRequest>()
            .With(x => x.EmployerId, employer.Id)
            .With(x => x.Description, string.Empty)
            .Create();
        DbContext.ChangeTracker.Clear();

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var employerInDb = await DbContext.Employers.FirstOrDefaultAsync(x => x.Id == employer.Id);
        employerInDb.Should().NotBeNull();
        employerInDb.Description.Should().Be(null);
        employerInDb.CompanyName.Should().Be(employer.CompanyName);
        employerInDb.RegisteredOn.Should().Be(employer.RegisteredOn);
    }

    [Fact]
    public async Task Handle_ShouldUpdateEmployer_WhenDescriptionIsNull()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        await DbContext.Employers.AddAsync(employer);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateEmployersCommandRequest>()
            .With(x => x.EmployerId, employer.Id)
            .Create();
        request.Description = null;

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var employerInDb = await DbContext.Employers.FirstOrDefaultAsync(x => x.Id == employer.Id);
        employerInDb.Should().NotBeNull();
        employerInDb.Description.Should().Be(null);
        employerInDb.CompanyName.Should().Be(employer.CompanyName);
        employerInDb.RegisteredOn.Should().Be(employer.RegisteredOn);
    }
}