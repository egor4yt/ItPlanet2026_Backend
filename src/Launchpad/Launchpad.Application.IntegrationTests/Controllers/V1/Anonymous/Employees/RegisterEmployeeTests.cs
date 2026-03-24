using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.IntegrationTests.Abstractions;
using Launchpad.Domain.Entities;
using Launchpad.Shared;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.IntegrationTests.Controllers.V1.Anonymous.Employees;

public class RegisterEmployeeTests(ApiWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task RegisterEmployee_Should_Register_And_SaveToDatabase()
    {
        // Arrange
        var request = Fixture.Build<CreateEmployeeBody>()
            .With(x => x.Email, $"{Guid.NewGuid()}@mail.ru")
            .Create();

        // Act
        var response = await HttpClient.PostAsJsonAsync("/employees", request);

        // Assert
        var employeeInDb = await ApplicationDbContext.Employees.FirstOrDefaultAsync(x => x.Email == request.Email);

        response.StatusCode.Should().Be(HttpStatusCode.Created, await response.Content.ReadAsStringAsync());

        employeeInDb.Should().NotBeNull();
        employeeInDb.FirstName.Should().Be(request.FirstName);
        employeeInDb.LastName.Should().Be(request.LastName);
        employeeInDb.MiddleName.Should().Be(request.MiddleName);
        employeeInDb.PasswordHash.Should().Be(SecurityHelper.ComputeSha256Hash(request.Password));
    }

    [Fact]
    public async Task RegisterEmployee_Should_FailWithConflict()
    {
        // Arrange
        var existsEmployee = Fixture.Create<Employee>();
        await ApplicationDbContext.Employees.AddAsync(existsEmployee);
        await ApplicationDbContext.SaveChangesAsync();

        var request = Fixture.Build<CreateEmployeeBody>()
            .With(x => x.Email, existsEmployee.Email)
            .Create();

        // Act
        var response = await HttpClient.PostAsJsonAsync("/employees", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict, await response.Content.ReadAsStringAsync());
    }
}