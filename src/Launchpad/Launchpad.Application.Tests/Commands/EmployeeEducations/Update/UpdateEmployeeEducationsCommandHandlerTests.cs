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
        var employee = new Employee { FirstName = "A", LastName = "B", Email = "update@example.com", PasswordHash = "h" };
        var educationLevel = new EducationLevel { Id = 100, Title = "Bachelor" };
        var otherEducationLevel = new EducationLevel { Id = 101, Title = "Master" };
        await DbContext.Employees.AddAsync(employee);
        await DbContext.EducationLevels.AddAsync(educationLevel);
        await DbContext.EducationLevels.AddAsync(otherEducationLevel);
        await DbContext.SaveChangesAsync();

        var education = new EmployeeEducation
        {
            EmployeeId = employee.Id,
            EducationLevelId = educationLevel.Id,
            Organization = "Old Org",
            Faculty = "Old Faculty",
            Specialization = "Old Spec",
            CompletionYear = 2020
        };
        await DbContext.EmployeeEducations.AddAsync(education);
        await DbContext.SaveChangesAsync();

        var request = new UpdateEmployeeEducationsCommandRequest
        {
            EducationId = education.Id,
            EducationLevelId = otherEducationLevel.Id,
            Organization = "New Org",
            Faculty = "New Faculty",
            Specialization = "New Spec",
            CompletionYear = 2024
        };

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
