using Launchpad.Application.Commands.Employers.Authorize;
using Launchpad.Application.Exceptions;
using Launchpad.Application.Tests.Abstractions;
using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Commands.Employers.Authorize;

public class AuthorizeEmployerCommandHandlerTests : BaseApplicationTest
{
    private readonly AuthorizeEmployersCommandHandler _handler;

    public AuthorizeEmployerCommandHandlerTests()
    {
        _handler = new AuthorizeEmployersCommandHandler(DbContext);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var employer = Fixture.Create<Employer>();
        await DbContext.Employers.AddAsync(employer);
        await DbContext.SaveChangesAsync();

        var request = Fixture.Build<AuthorizeEmployersCommandRequest>()
            .With(x => x.Email, employer.Email)
            .With(x => x.PasswordHash, employer.PasswordHash)
            .With(x => x.JwtDescriptorDetails, DefaultJwtDetails)
            .Create();

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.EmployerId.Should().Be(employer.Id);
        response.BearerToken.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_ShouldThrowForbiddenException_WhenCredentialsAreInvalid()
    {
        // Arrange
        var request = Fixture.Build<AuthorizeEmployersCommandRequest>()
            .With(x => x.JwtDescriptorDetails, DefaultJwtDetails)
            .Create();

        // Act
        var act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>()
            .WithMessage("Forbidden");
    }
}