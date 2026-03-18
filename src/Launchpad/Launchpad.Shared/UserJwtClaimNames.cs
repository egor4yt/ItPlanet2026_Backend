using System.IdentityModel.Tokens.Jwt;

namespace Launchpad.Shared;

public static class UserJwtClaimNames
{
    /* Custom claim types */
    public const string ProfileId = "ProfileId";
    public const string ContactEmail = "ContactEmail";
    public const string ProfileRole = "ProfileRole";

    /* RFC claim types */
    public const string JsonTokenIdentifier = JwtRegisteredClaimNames.Jti;
}