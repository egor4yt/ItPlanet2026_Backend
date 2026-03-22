using Launchpad.Application.Commands.Employees.Create;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employees.Create;

public class CreateEmployeesCommandHandlerTests : BaseUnitTest
{
    private readonly CreateEmployeesCommandHandler _handler;

    public CreateEmployeesCommandHandlerTests()
    {
        _handler = new CreateEmployeesCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldCreateEmployee_WhenRequestIsValid()
    {
        // Arrange
        var request = Fixture.Build<CreateEmployeesCommandRequest>()
            .With(x => x.JwtDescriptorDetails, DefaultJwtDetails)
            .Create();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.EmployeeId.Should().BeGreaterThan(0);
        response.BearerToken.Should().NotBeNullOrEmpty();

        var employeeInDb = await DbContext.Employees.FirstOrDefaultAsync(x => x.Id == response.EmployeeId);
        employeeInDb.Should().NotBeNull();
        employeeInDb!.FirstName.Should().Be(request.FirstName);
        employeeInDb.LastName.Should().Be(request.LastName);
        employeeInDb.MiddleName.Should().Be(request.MiddleName);
        employeeInDb.Email.Should().Be(request.Email);
        employeeInDb.PasswordHash.Should().Be(request.PasswordHash);
    }

    [Fact]
    public async Task Handle_ShouldThrowConflictException_WhenEmailAlreadyExists()
    {
        // Arrange
        var existingEmployee = Fixture.Create<Employee>();

        await DbContext.Employees.AddAsync(existingEmployee);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<CreateEmployeesCommandRequest>()
            .With(x => x.Email, existingEmployee.Email)
            .With(x => x.JwtDescriptorDetails, DefaultJwtDetails)
            .Create();

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("EmployeeAlreadyExists");
    }
}