using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Configs;

public class JwtService : IJwtService
{
    private readonly string _secretKey;
    private readonly int _expireMinutes;

    public JwtService(IConfiguration configuration)
    {
        _secretKey = configuration["JwtSettings:SecretKey"];
        _expireMinutes = int.Parse(configuration["JwtSettings:Expiration"]);
    }

    public string GenerateJwtToken(string username, Dictionary<string, object>? additionalClaims = null)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, 
                new DateTimeOffset(vietnamTime).ToUnixTimeSeconds().ToString(), 
                ClaimValueTypes.Integer64),
            new("role", "ADMIN")
        };

        if (additionalClaims != null)
        {
            foreach (var pair in additionalClaims)
            {
                claims.Add(new Claim(pair.Key, pair.Value?.ToString() ?? ""));
            }
        }

        var token = new JwtSecurityToken(
            claims: claims,
            expires: vietnamTime.AddMinutes(_expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}