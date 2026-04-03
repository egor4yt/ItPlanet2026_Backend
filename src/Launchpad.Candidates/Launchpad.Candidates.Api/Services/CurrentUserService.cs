using System.Security.Claims;
using Launchpad.Candidates.Api.Services.Interfaces;

namespace Launchpad.Candidates.Api.Services;

/// <inheritdoc />
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public Guid IdentityId
    {
        get
        {
            var stringUserId = _user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(stringUserId) || !Guid.TryParse(stringUserId, out var guidUserId)) throw new InvalidOperationException("User id was null");

            return guidUserId;
        }
    }

    /// <inheritdoc />
    public string IdentityEmail => _user?.FindFirstValue(ClaimTypes.Email) ?? throw new InvalidOperationException("User id was null");

    /// <inheritdoc />
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc />
    public bool IsInRole(string role)
    {
        return _user?.IsInRole(role) ?? false;
    }
}