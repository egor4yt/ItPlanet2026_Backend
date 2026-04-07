using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Launchpad.Candidates.Api.Authentication;

/// <summary>
///     Authentication handler used for tests
/// </summary>
public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    /// <inheritdoc />
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    /// <inheritdoc />
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeader) || !authHeader.ToString().StartsWith("Test ", StringComparison.InvariantCultureIgnoreCase))
            return Task.FromResult(AuthenticateResult.NoResult());

        var userId = authHeader.ToString()["Test ".Length..].Trim();
        if (!Guid.TryParse(userId, out _)) return Task.FromResult(AuthenticateResult.Fail("Invalid User ID format"));

        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}