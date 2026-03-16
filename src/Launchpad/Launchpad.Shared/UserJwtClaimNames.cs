using System.IdentityModel.Tokens.Jwt;

namespace Launchpad.Shared;

public static class UserJwtClaimNames
{
    /* Custom claim types */
    public const string UserId = "UserId";
    public const string UserEmail = "UserEmail";

    /* RFC claim types */
    public const string JsonTokenIdentifier = JwtRegisteredClaimNames.Jti;
}