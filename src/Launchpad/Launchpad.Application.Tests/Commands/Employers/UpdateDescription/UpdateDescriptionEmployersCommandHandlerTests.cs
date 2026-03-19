using Launchpad.Application.Commands.Employers.UpdateDescription;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employers.UpdateDescription;

public class UpdateDescriptionEmployersCommandHandlerTests : BaseApplicationTest
{
    private readonly UpdateDescriptionEmployersCommandHandler _handler;

    public UpdateDescriptionEmployersCommandHandlerTests()
    {
        _handler = new UpdateDescriptionEmployersCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldUpdateDescriptionEmployer_WhenRequestIsValid()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        await DbContext.Employers.AddAsync(employer);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateDescriptionEmployersCommandRequest>()
            .With(x => x.EmployerId, employer.Id)
            .Create();

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var employerInDb = await DbContext.Employers.FirstOrDefaultAsync(x => x.Id == employer.Id);
        employerInDb.Should().NotBeNull();
        employerInDb.Description.Should().Be(request.Description);
        employerInDb.CompanyName.Should().Be(employer.CompanyName);
        employerInDb.RegisteredOn.Should().Be(employer.RegisteredOn);
    }

    [Fact]
    public async Task Handle_ShouldUpdateDescriptionEmployer_WhenDescriptionIsEmpty()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        await DbContext.Employers.AddAsync(employer);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<UpdateDescriptionEmployersCommandRequest>()
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
    public async Task Handle_ShouldUpdateDescriptionEmployer_WhenDescriptionIsNull()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        await DbContext.Employers.AddAsync(employer);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateDescriptionEmployersCommandRequest>()
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