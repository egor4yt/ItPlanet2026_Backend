using Launchpad.Application.Commands.Employers.Create;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Application.Tests.Commands.Employers.Create;

public class CreateEmployersCommandHandlerTests : BaseUnitTest
{
    private readonly CreateEmployersCommandHandler _handler;

    public CreateEmployersCommandHandlerTests(PostgisFixture postgis) : base(postgis)
    {
        _handler = new CreateEmployersCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldCreateEmployer_WhenRequestIsValid()
    {
        // Arrange
        var request = Fixture.Build<CreateEmployersCommandRequest>()
            .With(x => x.JwtDescriptorDetails, DefaultJwtDetails)
            .Create();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.EmployerId.Should().BeGreaterThan(0);
        response.BearerToken.Should().NotBeNullOrEmpty();

        var employerInDb = await DbContext.Employers.FirstOrDefaultAsync(x => x.Id == response.EmployerId);
        employerInDb.Should().NotBeNull();
        employerInDb.CompanyName.Should().Be(request.CompanyName);
        employerInDb.Email.Should().Be(request.Email);
        employerInDb.PasswordHash.Should().Be(request.PasswordHash);
    }

    [Fact]
    public async Task Handle_ShouldThrowConflictException_WhenEmailAlreadyExists()
    {
        // Arrange
        var existingEmail = "existing@example.com";
        var existingEmployer = Fixture.Create<Employer>();
        existingEmployer.Email = existingEmail;

        await DbContext.Employers.AddAsync(existingEmployer);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<CreateEmployersCommandRequest>()
            .With(x => x.Email, existingEmail)
            .With(x => x.JwtDescriptorDetails, DefaultJwtDetails)
            .Create();

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("EmployerAlreadyExists");
    }
}