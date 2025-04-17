namespace WebApplication1.Repository;

public class JwtSettings
{
    public int Expiration { get; set; }
    public string SecretKey { get; set; } = string.Empty;
}