using System.Net;
using System.Text.Json;

namespace Shop.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Business validation error");
            await WriteErrorResponse(context, HttpStatusCode.BadRequest, ex.Message, "BUSINESS_RULE_VIOLATION");
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error");
            await WriteErrorResponse(context, HttpStatusCode.BadRequest, ex.Message, "VALIDATION_ERROR");
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access");
            await WriteErrorResponse(context, HttpStatusCode.Unauthorized, ex.Message, "UNAUTHORIZED");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteErrorResponse(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.", "INTERNAL_SERVER_ERROR");
        }
    }

    private static async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string message, string errorCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new
        {
            errorCode,
            message,
            status = context.Response.StatusCode,
            timestamp = DateTime.UtcNow
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
