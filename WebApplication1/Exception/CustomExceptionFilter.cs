using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly ILogger<CustomExceptionFilter> _logger;

    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Exception caught by CustomExceptionFilter");

        if (context.Exception is ErrorResponse errorResponse)
        {
            context.Result = new ObjectResult(DefaultRes<string>.Res(
                errorResponse.ErrorCode,
                errorResponse.Message,
                errorResponse.FriendlyMessage))
            {
                StatusCode = errorResponse.ErrorCode
            };
        }
        else
        {
            context.Result = new ObjectResult(DefaultRes<string>.Res(
                500,
                "Internal Server Error",
                "Something went wrong"))
            {
                StatusCode = 500
            };
        }

        context.ExceptionHandled = true;
    }
}