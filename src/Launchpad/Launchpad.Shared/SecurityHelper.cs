using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Launchpad.Shared;

public class SecurityHelper
{
    public static string ComputeSha256Hash(string rawData)
    {
        var inputBytes = Encoding.UTF8.GetBytes(rawData);
        var hashBytes = SHA256.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }

    public static string GenerateJwtToken(JwtDescriptorDetails jwtDescriptorDetails, JwtDetails jwtDetails)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtDescriptorDetails.Key);

        var claims = new List<Claim>
        {
            /* Custom claim types */
            new Claim(UserJwtClaimNames.ContactEmail, jwtDetails.ContactEmail),
            new Claim(UserJwtClaimNames.ProfileId, jwtDetails.ProfileId),
            new Claim(UserJwtClaimNames.ProfileRole, jwtDetails.ProfileRole),

            /* RFC claim types */
            new Claim(UserJwtClaimNames.JsonTokenIdentifier, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = jwtDescriptorDetails.Audience,
            Expires = DateTime.UtcNow.AddHours(jwtDescriptorDetails.TokenLifetimeInHours),
            Issuer = jwtDescriptorDetails.Issuer,
            IssuedAt = DateTime.UtcNow,
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKeyBytes), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token)!;

        return jwt;
    }
}