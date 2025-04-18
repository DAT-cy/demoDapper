
using WebApplication1.Dto;

namespace WebApplication1.Service.AuthService;

public interface IAuthService
{
    string Login(LoginRequest loginRequest);
}