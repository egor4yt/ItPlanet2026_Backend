using Launchpad.Application.Commands.EmployeeProjects.Create;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.EmployeeProjects.Create;

public class CreateEmployeeProjectsCommandHandlerTests : BaseUnitTest
{
    private readonly CreateEmployeeProjectsCommandHandler _handler;

    public CreateEmployeeProjectsCommandHandlerTests(PostgisFixture postgis) : base(postgis)
    {
        _handler = new CreateEmployeeProjectsCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldCreateProject_WhenRequestIsValid()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();

        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<CreateEmployeeProjectsCommandRequest>()
            .With(x => x.EmployeeId, employee.Id)
            .Create();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();

        var databaseEntity = await DbContext.EmployeeProjects.FirstOrDefaultAsync(x => x.EmployeeId == employee.Id);
        databaseEntity.Should().NotBeNull();
        databaseEntity.Title.Should().Be(request.Title);
        databaseEntity.Specialization.Should().Be(request.Specialization);
        databaseEntity.DateFrom.Should().Be(request.DateFrom);
        databaseEntity.DateTo.Should().Be(request.DateTo);
        databaseEntity.Description.Should().Be(request.Description);
        databaseEntity.Link.Should().Be(request.Link);
    }
}