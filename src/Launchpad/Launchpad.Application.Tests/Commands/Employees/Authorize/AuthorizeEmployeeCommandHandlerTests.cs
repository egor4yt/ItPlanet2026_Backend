using Launchpad.Application.Commands.Employees.Authorize;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Commands.Employees.Authorize;

public class AuthorizeEmployeeCommandHandlerTests : BaseApplicationTest
{
    private readonly AuthorizeEmployeeCommandHandler _handler;

    public AuthorizeEmployeeCommandHandlerTests()
    {
        _handler = new AuthorizeEmployeeCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var email = "test@example.com";
        var passwordHash = "correct_hash";
        var employee = new Employee
        {
            Email = email,
            PasswordHash = passwordHash,
            FirstName = "Test",
            LastName = "User"
        };
        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();

        var request = new AuthorizeEmployeeCommandRequest
        {
            Email = email,
            PasswordHash = passwordHash,
            JwtDescriptorDetails = DefaultJwtDetails
        };

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.EmployeeId.Should().Be(employee.Id);
        response.BearerToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_ShouldThrowForbiddenException_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = new AuthorizeEmployeeCommandRequest
        {
            Email = "nonexistent@example.com",
            PasswordHash = "wrong_hash",
            JwtDescriptorDetails = DefaultJwtDetails
        };

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>()
            .WithMessage("Forbidden");
    }
}
