using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Launchpad.Application.Abstrcations;
using Launchpad.Application.IntegrationTests.Abstractions;
using Launchpad.Application.Queries.Employers.Search;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.IntegrationTests.Controllers.V1.Curators.Employers;

public class SearchEmployersTests(ApiWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    private const string BaseUrl = "employers";

    [Fact]
    public async Task Search_ShouldReturnFilteredResults_WhenCuratorIsAuthenticated()
    {
        // Arrange
        var curator = Fixture.Create<Curator>();
        Authenticate(curator);

        const int targetStatusId = Domain.Metadata.EmployerVerificationStatusId.Pending;
        const string companyNamePart = "lpha";

        var matchingEmployers = Fixture.CreateMany<Employer>(2).ToList();

        foreach (var emp in matchingEmployers)
        {
            emp.CompanyName = "Alpha Systems";
            emp.Verification = Fixture.Create<EmployerVerification>();
            emp.Verification.StatusId = targetStatusId;
        }

        var otherStatusEmployer = Fixture.Create<Employer>();
        otherStatusEmployer.CompanyName = "Alpha Systems";
        otherStatusEmployer.Verification = Fixture.Create<EmployerVerification>();
        otherStatusEmployer.Verification.StatusId = Domain.Metadata.EmployerVerificationStatusId.Rejected;

        var otherNameEmployer = Fixture.Create<Employer>();
        otherNameEmployer.Verification = Fixture.Create<EmployerVerification>();
        otherNameEmployer.Verification.StatusId = targetStatusId;

        await ApplicationDbContext.Employers.AddRangeAsync(matchingEmployers);
        await ApplicationDbContext.Employers.AddAsync(otherStatusEmployer);
        await ApplicationDbContext.Employers.AddAsync(otherNameEmployer);
        await ApplicationDbContext.SaveChangesAsync();

        var url = $"/employers?" +
                  $"companyName={companyNamePart}&" +
                  $"verificationStatusId={targetStatusId}&" +
                  $"pageNumber=1" +
                  $"&pageSize=10";

        // Act
        var response = await HttpClient.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());

        var result = await response.Content.ReadFromJsonAsync<PagedResult<SearchEmployersQueryResponse>>();

        result.Should().NotBeNull();
        result.TotalItems.Should().Be(2);
        result.Items.Should().HaveCount(2);
        result.Items.Should().OnlyContain(x => x.Name.Contains(companyNamePart));
    }

    [Fact]
    public async Task Search_ShouldReturnEmptyList_WhenNoMatchesFound()
    {
        // Arrange
        var curator = Fixture.Create<Curator>();
        Authenticate(curator);

        var employers = Fixture.CreateMany<Employer>(3).ToList();
        await ApplicationDbContext.Employers.AddRangeAsync(employers);
        await ApplicationDbContext.SaveChangesAsync();

        const string url = "/employers?" +
                           "companyName=NonExistentName&" +
                           "pageNumber=1&" +
                           "pageSize=10";

        // Act
        var response = await HttpClient.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());
        var result = await response.Content.ReadFromJsonAsync<PagedResult<SearchEmployersQueryResponse>>();
        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalItems.Should().Be(0);
    }

    [Fact]
    public async Task Search_ShouldReturnForbidden_WhenUserIsEmployee()
    {
        // Arrange
        var employee = Fixture.Create<Employee>();
        Authenticate(employee);

        // Act
        var response = await HttpClient.GetAsync("/employers?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Search_ShouldReturnForbidden_WhenUserIsEmployer()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        Authenticate(employer);

        // Act
        var response = await HttpClient.GetAsync("/employers?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Search_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated()
    {
        // Arrange

        // Act
        var response = await HttpClient.GetAsync("/employers?pageNumber=1&pageSize=10");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized, await response.Content.ReadAsStringAsync());
    }

    [Theory]
    [InlineData(0, 10)] // PageNumber < 1
    [InlineData(1, 0)] // PageSize < 1
    [InlineData(1, 101)] // PageSize > 100
    public async Task Search_ShouldReturnBadRequest_WhenParametersAreInvalid(int pageNumber, int pageSize)
    {
        // Arrange
        var curator = Fixture.Create<Curator>();
        Authenticate(curator);

        var url = $"/employers?pageNumber={pageNumber}&pageSize={pageSize}";

        // Act
        var response = await HttpClient.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Search_ShouldReturnCorrectPaginationMetadata()
    {
        // Arrange
        var curator = Fixture.Create<Curator>();
        Authenticate(curator);

        var employersBeforeTest = await ApplicationDbContext.Employers.CountAsync();

        var employers = Fixture.CreateMany<Employer>(15).ToList();
        await ApplicationDbContext.Employers.AddRangeAsync(employers);
        await ApplicationDbContext.SaveChangesAsync();

        const int pageSize = 5;
        const int pageNumber = 2;
        var url = $"/employers?pageNumber={pageNumber}&pageSize={pageSize}";

        // Act
        var response = await HttpClient.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());
        var result = await response.Content.ReadFromJsonAsync<PagedResult<SearchEmployersQueryResponse>>();

        var totalCount = employersBeforeTest + 15;
        result.Should().NotBeNull();
        result.CurrentPage.Should().Be(pageNumber);
        result.TotalItems.Should().Be(totalCount);
        result.TotalPages.Should().Be((int)Math.Ceiling(totalCount / (double)pageSize));
        result.Items.Should().HaveCount(pageSize);
    }
}