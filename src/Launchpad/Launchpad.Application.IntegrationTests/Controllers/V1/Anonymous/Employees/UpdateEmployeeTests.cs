using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.IntegrationTests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.IntegrationTests.Controllers.V1.Anonymous.Employees;

public class UpdateEmployeeTests(ApiWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Update_Should_UpdateDataInDatabase_WhenUserIsAuthenticated()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();

        await ApplicationDbContext.Employees.AddAsync(employee);
        await ApplicationDbContext.SaveChangesAsync();

        Authenticate(employee);

        var request = Fixture.Create<UpdateEmployeeBody>();

        // Act
        var response = await HttpClient.PutAsJsonAsync("employees", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent, await response.Content.ReadAsStringAsync());

        // Сбрасываем кэш EF, чтобы получить данные напрямую из БД
        var employeeInDb = await ApplicationDbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == employee.Id);

        employeeInDb.Should().NotBeNull();
        employeeInDb!.FirstName.Should().Be(request.FirstName);
        employeeInDb.LastName.Should().Be(request.LastName);
        employeeInDb.MiddleName.Should().Be(request.MiddleName);
        employeeInDb.IsMale.Should().Be(request.IsMale);
        employeeInDb.BirthDate.Should().Be(request.BirthDate);
    }

    [Fact]
    public async Task Update_Should_ReturnUnauthorized_WhenUserIsNotAuthenticated()
    {
        // Arrange
        HttpClient.DefaultRequestHeaders.Authorization = null;
        var request = Fixture.Create<UpdateEmployeeBody>();

        // Act
        var response = await HttpClient.PutAsJsonAsync("employees", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized, await response.Content.ReadAsStringAsync());
    }
}