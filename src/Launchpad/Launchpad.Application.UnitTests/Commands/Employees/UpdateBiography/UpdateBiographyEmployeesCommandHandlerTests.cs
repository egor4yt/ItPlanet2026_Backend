using Launchpad.Application.Commands.Employees.UpdateBiography;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employees.UpdateBiography;

public class UpdateBiographyEmployeesCommandHandlerTests : BaseUnitTest
{
    private readonly UpdateBiographyEmployeesCommandHandler _handler;

    public UpdateBiographyEmployeesCommandHandlerTests(PostgisFixture postgis) : base(postgis)
    {
        _handler = new UpdateBiographyEmployeesCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldUpdateBiography_WhenRequestIsValid()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();
        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateBiographyEmployeesCommandRequest>()
            .With(x => x.EmployeeId, employee.Id)
            .Create();

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var employeeInDb = await DbContext.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
        employeeInDb.Should().NotBeNull();
        employeeInDb.Biography.Should().Be(request.Biography);
    }
}