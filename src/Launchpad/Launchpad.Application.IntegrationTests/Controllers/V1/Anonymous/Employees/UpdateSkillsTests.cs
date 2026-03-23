using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.IntegrationTests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.IntegrationTests.Controllers.V1.Anonymous.Employees;

public class UpdateSkillsTests(ApiWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task UpdateSkills_Should_ReplaceOldSkills_WithNewAndExistingOnes()
    {
        // Arrange
        var existingSystemSkill = Fixture.Create<Skill>();
        await ApplicationDbContext.Skills.AddAsync(existingSystemSkill);

        var oldSkill = Fixture.Create<Skill>();
        var employee = Fixture.Create<Employee>();
        employee.Skills.Add(oldSkill);

        await ApplicationDbContext.Employees.AddAsync(employee);
        await ApplicationDbContext.SaveChangesAsync();

        Authenticate(employee);
        var newSkillName = Fixture.Create<string>();

        var request = Fixture.Build<UpdateEmployeeSkillsBody>()
            .With(x => x.Skills, [
                new UpdateEmployeeSkillsBodyItem
                {
                    SkillId = null,
                    Title = newSkillName
                },
                new UpdateEmployeeSkillsBodyItem
                {
                    SkillId = existingSystemSkill.Id,
                    Title = Guid.NewGuid().ToString() // will be ignored
                }
            ])
            .Create();

        // Act
        var response = await HttpClient.PatchAsJsonAsync("employees/skills", request);

        // Assert
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var employeeInDb = await ApplicationDbContext.Employees
            .Include(x => x.Skills)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == employee.Id);

        employeeInDb.Should().NotBeNull();
        employeeInDb.Skills.Should().HaveCount(2);
        employeeInDb.Skills.Should().NotContain(s => s.Id == oldSkill.Id);
        employeeInDb.Skills.Should().ContainSingle(s => s.Id == existingSystemSkill.Id);
        employeeInDb.Skills.Should().ContainSingle(s => s.Title == newSkillName && s.IsSystemTag == false);
    }

    [Fact]
    public async Task UpdateSkills_Should_ReturnUnauthorized_WhenNoToken()
    {
        // Arrange
        var request = Fixture.Create<UpdateEmployeeSkillsBody>();
        HttpClient.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await HttpClient.PatchAsJsonAsync("employees/skills", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateSkills_Should_ReturnForbidden_WhenUserIsNotEmployee()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();

        await ApplicationDbContext.Employers.AddAsync(employer);
        await ApplicationDbContext.SaveChangesAsync();

        Authenticate(employer);

        var request = Fixture.Create<UpdateEmployeeSkillsBody>();

        // Act
        var response = await HttpClient.PatchAsJsonAsync("/employees/skills", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateSkills_Should_ReturnBadRequest_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();

        Authenticate(employer);

        var request = Fixture.Create<UpdateEmployeeSkillsBody>();

        // Act
        var response = await HttpClient.PatchAsJsonAsync("employees/skills", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}