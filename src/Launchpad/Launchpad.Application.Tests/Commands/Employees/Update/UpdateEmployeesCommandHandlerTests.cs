using Launchpad.Application.Commands.Employees.Update;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employees.Update;

public class UpdateEmployeesCommandHandlerTests : BaseApplicationTest
{
    private readonly UpdateEmployeesCommandHandler _handler;

    public UpdateEmployeesCommandHandlerTests()
    {
        _handler = new UpdateEmployeesCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldUpdate_WhenRequestIsValid()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();
        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateEmployeesCommandRequest>()
            .With(x => x.EmployeeId, employee.Id)
            .Create();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        response.Should().NotBeNull();

        var employeeInDb = await DbContext.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
        employeeInDb.Should().NotBeNull();
        employeeInDb.FirstName.Should().Be(request.FirstName);
        employeeInDb.LastName.Should().Be(request.LastName);
        employeeInDb.MiddleName.Should().Be(request.MiddleName);
        employeeInDb.BirthDate.Should().Be(request.BirthDate);
        employeeInDb.IsMale.Should().Be(request.IsMale);
    }
}