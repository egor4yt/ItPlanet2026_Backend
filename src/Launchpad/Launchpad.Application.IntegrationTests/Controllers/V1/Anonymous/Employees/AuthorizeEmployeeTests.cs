using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Launchpad.Api.Contracts.Employees;
using Launchpad.Application.Commands.Employees.Authorize;
using Launchpad.Application.IntegrationTests.Abstractions;
using Launchpad.Domain.Entities;
using Launchpad.Shared;

namespace Launchpad.Application.IntegrationTests.Controllers.V1.Anonymous.Employees;

public class AuthorizeEmployeeTests(ApiWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task AuthorizeEmployeeTests_Should_AuthorizeEmployee()
    {
        var password = Fixture.Create<string>();
        var employee = Fixture.Create<Employee>();
        employee.PasswordHash = SecurityHelper.ComputeSha256Hash(password);
        ApplicationDbContext.Employees.Add(employee);
        await ApplicationDbContext.SaveChangesAsync();

        // Arrange
        var request = Fixture.Build<AuthorizeEmployeeBody>()
            .With(x => x.Email, employee.Email)
            .With(x => x.Password, password)
            .Create();

        // Act
        var response = await HttpClient.PostAsJsonAsync("/employees/authorization", request);
        var responseDetails = await response.Content.ReadFromJsonAsync<AuthorizeEmployeeCommandResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseDetails.Should().NotBeNull();
        responseDetails.ProfileId.Should().Be(employee.Id);
    }

    [Fact]
    public async Task AuthorizeEmployeeTests_Should_AuthorizeSeededCurator()
    {
        // Arrange
        var request = Fixture.Build<AuthorizeEmployeeBody>()
            .With(x => x.Email, "admin@launchpad.ru")
            .With(x => x.Password, "admin")
            .Create();

        // Act
        var response = await HttpClient.PostAsJsonAsync("/employees/authorization", request);
        var responseDetails = await response.Content.ReadFromJsonAsync<AuthorizeEmployeeCommandResponse>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseDetails.Should().NotBeNull();
        responseDetails.ProfileId.Should().Be(-1);
    }
}