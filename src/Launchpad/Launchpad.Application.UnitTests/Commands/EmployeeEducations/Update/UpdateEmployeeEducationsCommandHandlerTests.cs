using Launchpad.Application.Commands.EmployeeEducations.Update;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.EmployeeEducations.Update;

public class UpdateEmployeeEducationsCommandHandlerTests : BaseUnitTest
{
    private readonly UpdateEmployeeEducationsCommandHandler _handler;

    public UpdateEmployeeEducationsCommandHandlerTests(PostgisFixture postgis) : base(postgis)
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

        var education = Fixture.Create<EmployeeEducation>();
        education.EmployeeId = employee.Id;
        education.EducationLevelId = educationLevel.Id;

        await DbContext.EmployeeEducations.AddAsync(education);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateEmployeeEducationsCommandRequest>()
            .With(x => x.EducationId, education.Id)
            .With(x => x.EducationLevelId, otherEducationLevel.Id)
            .Without(x => x.EmployerId)
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