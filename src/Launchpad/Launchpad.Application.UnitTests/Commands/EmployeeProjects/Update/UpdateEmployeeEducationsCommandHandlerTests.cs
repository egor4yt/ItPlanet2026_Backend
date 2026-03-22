using Launchpad.Application.Commands.EmployeeProjects.Update;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.EmployeeProjects.Update;

public class UpdateEmployeeProjectsCommandHandlerTests : BaseUnitTest
{
    private readonly UpdateEmployeeProjectsCommandHandler _handler;

    public UpdateEmployeeProjectsCommandHandlerTests()
    {
        _handler = new UpdateEmployeeProjectsCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldUpdateProject_WhenRequestIsValid()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();

        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var project = Fixture.Create<EmployeeProject>();
        project.EmployeeId = employee.Id;

        await DbContext.EmployeeProjects.AddAsync(project);
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();

        var request = Fixture.Build<UpdateEmployeeProjectsCommandRequest>()
            .With(x => x.ProjectId, project.Id)
            .Without(x => x.EmployerId)
            .Create();

        // Act
        await _handler.Handle(request, CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        // Assert
        var databaseEntity = await DbContext.EmployeeProjects.FirstOrDefaultAsync(x => x.Id == project.Id);
        databaseEntity.Should().NotBeNull();
        databaseEntity.Title.Should().Be(request.Title);
        databaseEntity.Specialization.Should().Be(request.Specialization);
        databaseEntity.DateFrom.Should().Be(request.DateFrom);
        databaseEntity.DateTo.Should().Be(request.DateTo);
        databaseEntity.Description.Should().Be(request.Description);
        databaseEntity.Link.Should().Be(request.Link);
    }
}