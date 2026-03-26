using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Launchpad.Api.Contracts.Shared;
using Launchpad.Api.Contracts.Vacancies;
using Launchpad.Application.Commands.Vacancies.Create;
using Launchpad.Application.IntegrationTests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Launchpad.Application.IntegrationTests.Controllers.V1.Employers.Vacancies;

public class CreateVacancyTests(ApiWebApplicationFactory factory) : BaseIntegrationTest(factory)
{
    private const string BaseUrl = "vacancies";

    [Fact]
    public async Task Create_ShouldReturnCreated_WhenDataIsValidAndEmployerIsApproved()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        employer.Verification = Fixture.Create<EmployerVerification>();
        employer.Verification.StatusId = Domain.Metadata.EmployerVerificationStatusId.Approved;

        var existingSkill = Fixture.Create<Skill>();

        await ApplicationDbContext.Employers.AddAsync(employer);
        await ApplicationDbContext.Skills.AddAsync(existingSkill);
        await ApplicationDbContext.SaveChangesAsync();

        Authenticate(employer);

        var newSkillName = Fixture.Create<string>();
        var randomPoint = Fixture.Create<Point>();
        var dates = Fixture.CreateMany<DateTime>(2).Order().ToList();
        var request = Fixture
            .Build<CreateVacancyBody>()
            .With(x => x.Location, new GeolocationPoint
            {
                Longitude = randomPoint.X,
                Latitude = randomPoint.Y
            })
            .With(x => x.TypeId, Domain.Metadata.VacancyTypeId.Intership)
            .With(x => x.Skills, [
                new CreateVacnacyBodySkill
                {
                    Id = existingSkill.Id,
                    Title = Guid.NewGuid().ToString() // will be ignored
                },
                new CreateVacnacyBodySkill
                {
                    Id = null,
                    Title = newSkillName
                }
            ])
            .With(x => x.WorkFormatIds, [Domain.Metadata.WorkFormatId.Office])
            .With(x => x.StartDate, dates[0])
            .With(x => x.EndDate, dates[1])
            .Create();

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/{employer.Id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created, await response.Content.ReadAsStringAsync());

        var result = await response.Content.ReadFromJsonAsync<CreateVacanciesCommandResponse>();
        result.Should().NotBeNull();
        result.VacancyId.Should().BeGreaterThan(0);

        var dbVacancy = await ApplicationDbContext.Vacancies
            .Include(v => v.WorkFormats)
            .Include(v => v.Skills)
            .FirstOrDefaultAsync(v => v.Id == result.VacancyId);

        dbVacancy.Should().NotBeNull();
        dbVacancy.EmployerId.Should().Be(employer.Id);
        dbVacancy.Title.Should().Be(request.Title);
        dbVacancy.Description.Should().Be(request.Description);
        dbVacancy.Location.X.Should().Be(request.Location.Longitude);
        dbVacancy.Location.Y.Should().Be(request.Location.Latitude);
        dbVacancy.StartDate.Should().Be(request.StartDate);
        dbVacancy.EndDate.Should().Be(request.EndDate);
        dbVacancy.TypeId.Should().Be(Domain.Metadata.VacancyTypeId.Intership);
        dbVacancy.WorkFormats.Should().ContainSingle(x => x.Id == Domain.Metadata.WorkFormatId.Office);
        dbVacancy.Skills.Should().HaveCount(2);
        dbVacancy.Skills.Should().ContainSingle(s => s.Id == existingSkill.Id);
        dbVacancy.Skills.Should().ContainSingle(s => s.Title == newSkillName && s.IsSystemTag == false);
    }

    [Fact]
    public async Task Create_ShouldReturnForbidden_WhenEmployerIsNotApproved()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        employer.Verification = Fixture.Create<EmployerVerification>();
        employer.Verification.StatusId = Domain.Metadata.EmployerVerificationStatusId.Pending;

        await ApplicationDbContext.Employers.AddAsync(employer);
        await ApplicationDbContext.SaveChangesAsync();

        Authenticate(employer);

        var dates = Fixture.CreateMany<DateTime>(2).Order().ToList();
        var requestBody = Fixture.Build<CreateVacancyBody>()
            .With(x => x.StartDate, dates[0])
            .With(x => x.EndDate, dates[1])
            .Create();

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/{employer.Id}", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden, await response.Content.ReadAsStringAsync());
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("NeedsVerification");
    }

    [Fact]
    public async Task Create_ShouldReturnForbidden_WhenEmployerTriesToCreateForAnotherEmployerId()
    {
        // Arrange
        var employerMe = Fixture.Create<Employer>();
        var employerOther = Fixture.Create<Employer>();

        await ApplicationDbContext.Employers.AddRangeAsync(employerMe, employerOther);
        await ApplicationDbContext.SaveChangesAsync();

        Authenticate(employerMe);

        var requestBody = Fixture.Create<CreateVacancyBody>();

        // Act - стучимся в чужой ID
        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/{employerOther.Id}", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden, await response.Content.ReadAsStringAsync());
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("UseYourProfileId");
    }

    [Fact]
    public async Task Create_ShouldReturnUnauthorized_WhenNotAuthenticated()
    {
        // Arrange
        var requestBody = Fixture.Create<CreateVacancyBody>();

        // Act
        var response = await HttpClient.PostAsJsonAsync($"{BaseUrl}/1", requestBody);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized, await response.Content.ReadAsStringAsync());
    }
}