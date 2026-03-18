using Launchpad.Application.Commands.Skills.AttachEmployee;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Skills.AttachEmployee;

public class AttachEmployeeSkillsCommandHandlerTests : BaseApplicationTest
{
    private readonly AttachEmployeeSkillsCommandHandler _handler;

    public AttachEmployeeSkillsCommandHandlerTests()
    {
        _handler = new AttachEmployeeSkillsCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldAttachNewAndExistingSkills_WhenRequestIsValid()
    {
        // Arrange
        var employee = Fixture.Build<Employee>()
            .Without(x => x.Id)
            .Without(x => x.Skills)
            .Create();
        var existingSkill = Fixture.Build<Skill>()
            .Without(x => x.Id)
            .With(x => x.Title, "C#")
            .Create();
        await DbContext.Employees.AddAsync(employee);
        await DbContext.Skills.AddAsync(existingSkill);
        await DbContext.SaveChangesAsync();

        var request = new AttachEmployeeSkillsCommandRequest
        {
            EmployeeId = employee.Id,
            Skills = new List<AttachEmployeeSkillsCommandRequestItem>
            {
                new() { SkillId = (int)existingSkill.Id, Title = existingSkill.Title }, // Existing
                new() { SkillId = null, Title = "Unit Testing" }         // New
            }
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        var employeeWithSkills = await DbContext.Employees
            .Include(x => x.Skills)
            .FirstAsync(x => x.Id == employee.Id);

        employeeWithSkills.Skills.Should().HaveCount(2);
        employeeWithSkills.Skills.Should().Contain(s => s.Title == "C#");
        employeeWithSkills.Skills.Should().Contain(s => s.Title == "Unit Testing");
    }

    [Fact]
    public async Task Handle_ShouldClearExistingSkills_WhenNewSkillsProvided()
    {
        // Arrange
        var oldSkill = Fixture.Build<Skill>()
            .Without(x => x.Id)
            .Create();
        var employee = Fixture.Build<Employee>()
            .Without(x => x.Id)
            .With(x => x.Skills, new List<Skill> { oldSkill })
            .Create();
        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();

        var request = new AttachEmployeeSkillsCommandRequest
        {
            EmployeeId = employee.Id,
            Skills = new List<AttachEmployeeSkillsCommandRequestItem>
            {
                new() { SkillId = null, Title = "New" }
            }
        };

        // Act
        await _handler.Handle(request, CancellationToken.None);

        // Assert
        var employeeWithSkills = await DbContext.Employees
            .Include(x => x.Skills)
            .FirstAsync(x => x.Id == employee.Id);

        employeeWithSkills.Skills.Should().HaveCount(1);
        employeeWithSkills.Skills.Should().Contain(s => s.Title == "New");
        employeeWithSkills.Skills.Should().NotContain(s => s.Title == oldSkill.Title);
    }

    [Fact]
    public async Task Handle_ShouldThrowBadRequestException_WhenEmployeeNotFound()
    {
        // Arrange
        var request = new AttachEmployeeSkillsCommandRequest
        {
            EmployeeId = 999,
            Skills = new List<AttachEmployeeSkillsCommandRequestItem>()
        };

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>()
            .WithMessage("EmployeeNotFound");
    }
}
