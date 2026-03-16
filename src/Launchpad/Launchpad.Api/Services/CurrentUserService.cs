using System.Security.Claims;
using Launchpad.Api.Services.Interfaces;
using Launchpad.Application.Exceptions;
using Launchpad.Shared;

namespace Launchpad.Api.Services;

/// <inheritdoc />
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public int UserId
    {
        get
        {
            var stringUserId = _user?.FindFirstValue(UserJwtClaimNames.UserId);
            if (string.IsNullOrWhiteSpace(stringUserId)) throw new ForbiddenException("User id was null");

            var longUserId = int.Parse(stringUserId);
            return longUserId;
        }
    }

    /// <inheritdoc />
    public string UserEmail => _user?.FindFirstValue(UserJwtClaimNames.UserEmail) ?? throw new ForbiddenException("User id was null");

    /// <inheritdoc />
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}