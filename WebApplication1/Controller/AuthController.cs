using ClassLibrary1.DAL.common;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Service.AuthService;
using YourNamespace.Common;

namespace WebApplication1.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController
{
    private readonly IAuthService _authService;
    
    
    public AuthController(
        IAuthService authService
)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public DefaultRes<string> Login([FromBody] LoginRequest request)
    {
        var user = _authService.Login(request);
        return DefaultRes<string>.Res(StatusCodeRespon.OK,ResponseMessage.CREATED, user);
    }
    

    
}