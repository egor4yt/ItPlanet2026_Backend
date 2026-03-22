using Launchpad.Application.Commands.EmployeeEducations.Create;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.EmployeeEducations.Create;

public class CreateEmployeeEducationsCommandHandlerTests : BaseUnitTest
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
        var employee = Fixture.Create<Employee>();
        var educationLevel = Fixture.Create<EducationLevel>();

        await DbContext.Employees.AddAsync(employee);
        await DbContext.EducationLevels.AddAsync(educationLevel);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<CreateEmployeeEducationsCommandRequest>()
            .With(x => x.EmployeeId, employee.Id)
            .With(x => x.EducationLevelId, educationLevel.Id)
            .Without(x => x.Organization)
            .Without(x => x.Faculty)
            .Without(x => x.Specialization)
            .Create();
        request.Organization = "MSU";
        request.Faculty = "CMC";
        request.Specialization = "CS";

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