using Launchpad.Application.Commands.Employees.Create;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employees.Create;

public class CreateEmployeeCommandHandlerTests : BaseApplicationTest
{
    private readonly CreateEmployeeCommandHandler _handler;

    public CreateEmployeeCommandHandlerTests()
    {
        _handler = new CreateEmployeeCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldCreateEmployee_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateEmployeeCommandRequest
        {
            FirstName = "Иван",
            LastName = "Иванов",
            MiddleName = "Иванович",
            Email = "ivanov@example.com",
            PasswordHash = "hashed_password",
            JwtDescriptorDetails = DefaultJwtDetails
        };

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
        var existingEmail = "existing@example.com";
        var existingEmployee = new Employee
        {
            FirstName = "Test",
            LastName = "User",
            Email = existingEmail,
            PasswordHash = "hash"
        };
        await DbContext.Employees.AddAsync(existingEmployee);
        await DbContext.SaveChangesAsync();

        var request = new CreateEmployeeCommandRequest
        {
            FirstName = "New",
            LastName = "User",
            Email = existingEmail, // Duplicate email
            PasswordHash = "new_hash",
            JwtDescriptorDetails = DefaultJwtDetails
        };

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("EmployeeAlreadyExists");
    }
}
