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

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        

        // Add extra claims
        if (additionalClaims != null)
        {
            foreach (var pair in additionalClaims)
            {
                if (pair.Value is List<string> roleList)
                {
                    foreach (var role in roleList)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
                else
                {
                    claims.Add(new Claim(pair.Key, pair.Value.ToString()));
                }            }
        }

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
