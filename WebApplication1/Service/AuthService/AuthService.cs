using WebApplication1.Configs;
using WebApplication1.Dto;
using WebApplication1.Entity;
using WebApplication1.Repository;
using WebApplication1.Service.AuthService;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly AuthRepository _authRepository;

    public AuthService(IJwtService jwtService, AuthRepository authRepository)
    {
        _jwtService = jwtService;
        _authRepository = authRepository;
    }

    public string Login(LoginRequest loginRequest)
    {
        var user = _authRepository.InsertUserAsync(loginRequest.Username, loginRequest.Password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        
        Dictionary<string, object> additionalClaims = new Dictionary<string, object>
        {
            { "UserId", user.Id },
        };
        // Tạo và trả về JWT
        return _jwtService.GenerateJwtToken(loginRequest.Username,additionalClaims );
    }
}