namespace WebApplication1.Configs;

public interface IJwtService
{
    string GenerateJwtToken(string username ,  Dictionary<string, object>? additionalClaims = null);

}