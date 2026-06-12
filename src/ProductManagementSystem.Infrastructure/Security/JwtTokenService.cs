using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagementSystem.Application.Interfaces.Security;
using ProductManagementSystem.Infrastructure.Configuration;

namespace ProductManagementSystem.Infrastructure.Security;

/// <summary>
/// Generates JWT access tokens and cryptographically secure refresh tokens.
/// </summary>
public sealed class JwtTokenService(IOptions<JwtOptions> jwtOptions) : IJwtTokenService
{
    private readonly JwtOptions options = jwtOptions.Value;

    /// <inheritdoc />
    public string GenerateAccessToken(ApplicationUser user)
    {
        var now = DateTime.UtcNow;
        var expiresOn = GetAccessTokenExpiry();
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            notBefore: now,
            expires: expiresOn,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <inheritdoc />
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    /// <inheritdoc />
    public DateTime GetAccessTokenExpiry()
    {
        return DateTime.UtcNow.AddMinutes(options.AccessTokenExpirationMinutes);
    }

    /// <inheritdoc />
    public DateTime GetRefreshTokenExpiry()
    {
        return DateTime.UtcNow.AddDays(options.RefreshTokenExpirationDays);
    }
}
