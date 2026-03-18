using Launchpad.Application.Commands.EmployeeEducations.Update;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.EmployeeEducations.Update;

public class UpdateEmployeeEducationsCommandHandlerTests : BaseApplicationTest
{
    private readonly UpdateEmployeeEducationsCommandHandler _handler;

    public UpdateEmployeeEducationsCommandHandlerTests()
    {
        _handler = new UpdateEmployeeEducationsCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldUpdateEducation_WhenRequestIsValid()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();
        var educationLevel = Fixture.Create<EducationLevel>();
        var otherEducationLevel = Fixture.Create<EducationLevel>();

        await DbContext.Employees.AddAsync(employee);
        await DbContext.EducationLevels.AddAsync(educationLevel);
        await DbContext.EducationLevels.AddAsync(otherEducationLevel);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var education = Fixture.Build<EmployeeEducation>()
            .With(x => x.EmployeeId, employee.Id)
            .With(x => x.EducationLevelId, educationLevel.Id)
            .Create();
        await DbContext.EmployeeEducations.AddAsync(education);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateEmployeeEducationsCommandRequest>()
            .With(x => x.EducationId, education.Id)
            .With(x => x.EducationLevelId, otherEducationLevel.Id)
            .Create();

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var educationInDb = await DbContext.EmployeeEducations.FirstOrDefaultAsync(x => x.Id == education.Id);
        educationInDb.Should().NotBeNull();
        educationInDb!.Organization.Should().Be(request.Organization);
        educationInDb.Faculty.Should().Be(request.Faculty);
        educationInDb.Specialization.Should().Be(request.Specialization);
        educationInDb.CompletionYear.Should().Be(request.CompletionYear);
        educationInDb.EducationLevelId.Should().Be(request.EducationLevelId);
    }
}