

using System.Security.Claims;
using ClassLibrary1.DAL.DbConnection;
using Dapper;
using Microsoft.Extensions.Logging;
using WebApplication1.Configs;
using WebApplication1.Dto;
using WebApplication1.Service.AuthService;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly DbConnection _db;
    private readonly SqlQueryLoader _queryLoader;
    private readonly ILogger<AuthService> _logger;


    public AuthService(IJwtService jwtService, DbConnection db , SqlQueryLoader queryLoader , ILogger<AuthService> logger)
    {
        _jwtService = jwtService;
        _db =  db;
        _queryLoader = queryLoader;
        _logger = logger;
    }

    public string Login(LoginRequest loginRequest)
    {
        var query = _queryLoader.GetQueryFromXml("getUserByUserNameAndPassword");
        var result = _db.QueryWithConnection(conn =>
            conn.QueryFirstOrDefault<UserDto>(query, new
            {
                username = loginRequest.Username,
                password = loginRequest.Password
                
            }));

        if (result == null)
        {
            throw new ErrorResponse(ErrorCode.USER_NOT_FOUND);
        }
        
        List<string> userRoles = new List<string> { "ADMIN", "USER" };

        Dictionary<string, object> additionalClaims = new Dictionary<string, object>
        {
            { ClaimTypes.Role, userRoles  },
        };
        return _jwtService.GenerateJwtToken(result.Username,additionalClaims );
    }
}