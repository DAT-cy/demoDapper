using Microsoft.IdentityModel.Tokens;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuthorizationMiddleware> _logger;

    public AuthorizationMiddleware(RequestDelegate next, ILogger<AuthorizationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Debug logging
        var user = context.User;
        
        if (user.Identity.IsAuthenticated)
        {
            _logger.LogInformation("User is authenticated");
            _logger.LogInformation("Roles: {Roles}", string.Join(", ", user.Claims.Where(c => c.Type == "role").Select(c => c.Value)));
        }
        else
        {
            _logger.LogInformation("Roles: {Roles}", string.Join(", ", user.Claims.Where(c => c.Type == "role").Select(c => c.Value)));
            _logger.LogWarning("User is not authenticated");
            _logger.LogInformation("Authorization header: {Auth}", context.Request.Headers["Authorization"].ToString());
        }

        try
        {
            await _next(context);
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogWarning("Token invalid: {Message}", ex.Message);
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Invalid Token",
                message = "Token không hợp lệ hoặc đã hết hạn"
            });
        }
    }
}