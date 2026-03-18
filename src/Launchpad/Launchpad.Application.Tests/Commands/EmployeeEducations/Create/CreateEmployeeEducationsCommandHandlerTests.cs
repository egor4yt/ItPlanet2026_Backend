using Launchpad.Application.Commands.EmployeeEducations.Create;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.EmployeeEducations.Create;

public class CreateEmployeeEducationsCommandHandlerTests : BaseApplicationTest
{
    private readonly CreateEmployeeEducationsCommandHandler _handler;

    public CreateEmployeeEducationsCommandHandlerTests()
    {
        _handler = new CreateEmployeeEducationsCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldCreateEducation_WhenRequestIsValid()
    {
        // Arrange
        var employee = new Employee { FirstName = "A", LastName = "B", Email = "a@b.com", PasswordHash = "h" };
        var educationLevel = new EducationLevel { Title = "Bachelor" };
        await DbContext.Employees.AddAsync(employee);
        await DbContext.EducationLevels.AddAsync(educationLevel);
        await DbContext.SaveChangesAsync();

        var request = new CreateEmployeeEducationsCommandRequest
        {
            EmployeeId = employee.Id,
            EducationLevelId = educationLevel.Id,
            Organization = "MSU",
            Faculty = "CMC",
            Specialization = "CS",
            CompletionYear = 2025
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        
        var educationInDb = await DbContext.EmployeeEducations.FirstOrDefaultAsync(x => x.EmployeeId == employee.Id);
        educationInDb.Should().NotBeNull();
        educationInDb!.Organization.Should().Be(request.Organization);
        educationInDb.Faculty.Should().Be(request.Faculty);
        educationInDb.Specialization.Should().Be(request.Specialization);
        educationInDb.CompletionYear.Should().Be(request.CompletionYear);
        educationInDb.EducationLevelId.Should().Be(request.EducationLevelId);
    }
}
