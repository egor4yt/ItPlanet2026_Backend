using System.Security.Claims;
using Launchpad.Api.Services.Interfaces;
using Launchpad.Application.Exceptions;

namespace Launchpad.Api.Services;

/// <inheritdoc />
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public long ProfileId
    {
        get
        {
            var stringUserId = _user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(stringUserId)) throw new ForbiddenException("User id was null");

            var longUserId = long.Parse(stringUserId);
            return longUserId;
        }
    }

    /// <inheritdoc />
    public string ContactEmail => _user?.FindFirstValue(ClaimTypes.Email) ?? throw new ForbiddenException("User id was null");

    /// <inheritdoc />
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc />
    public bool IsInRole(string role)
    {
        return _user?.IsInRole(role) ?? false;
    }
}