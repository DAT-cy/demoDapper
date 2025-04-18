using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtBearerEventsHandler
{
    

    public static  Action<JwtBearerOptions> GetJwtBearerOptions(IConfiguration configuration)
    {
        return options =>
        {
            var secretKey = configuration["JwtSettings:SecretKey"];

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                RoleClaimType = ClaimTypes.Role
            };

            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context =>
                {
                    var principal = context.Principal;

                    if (principal != null)
                    {
                        var roleClaims = principal.FindAll(ClaimTypes.Role).ToList();

                        if (roleClaims.Count != 0)
                        {
                            foreach (var roleClaim in roleClaims)
                            {
                                principal.AddIdentity(new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Role, roleClaim.Value)
                                }));
                            }
                        }
                    }

                    return Task.CompletedTask;
                },
                OnMessageReceived = context =>
                {
                    var authHeader = context.Request.Headers["Authorization"].ToString().Trim();

                    if (!string.IsNullOrEmpty(authHeader))
                    {
                        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = authHeader.Split(" ")[1].Trim();
                        }
                        else
                        {
                            context.Token = authHeader.Trim();
                        }
                    }

                    return Task.CompletedTask;
                }
            };
        };
    }
    }
