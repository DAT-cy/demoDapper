using ClassLibrary1.DAL.common;
using Microsoft.AspNetCore.Authorization;
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
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public DefaultRes<string> Login([FromBody] LoginRequest request)
    {
        var user = _authService.Login(request);
        return DefaultRes<string>.Res(StatusCodeRespon.OK,ResponseMessage.CREATED, user);
    }
    

    
}