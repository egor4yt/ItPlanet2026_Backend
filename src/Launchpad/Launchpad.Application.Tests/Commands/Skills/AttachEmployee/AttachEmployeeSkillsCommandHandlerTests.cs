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
        var employee = new Employee { FirstName = "A", LastName = "B", Email = "a@b.com", PasswordHash = "h" };
        var existingSkill = new Skill { Title = "C#", IsSystemTag = true };
        await DbContext.Employees.AddAsync(employee);
        await DbContext.Skills.AddAsync(existingSkill);
        await DbContext.SaveChangesAsync();

        var request = new AttachEmployeeSkillsCommandRequest
        {
            EmployeeId = employee.Id,
            Skills = new List<AttachEmployeeSkillsCommandRequestItem>
            {
                new() { SkillId = (int)existingSkill.Id, Title = "C#" }, // Existing
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
        var oldSkill = new Skill { Title = "Old", IsSystemTag = false };
        var employee = new Employee 
        { 
            FirstName = "A", LastName = "B", Email = "a@b.com", PasswordHash = "h",
            Skills = new List<Skill> { oldSkill }
        };
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
        employeeWithSkills.Skills.Should().NotContain(s => s.Title == "Old");
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
