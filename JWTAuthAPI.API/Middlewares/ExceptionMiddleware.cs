using JWTAuthAPI.Application.Common.Exceptions;
using JWTAuthAPI.Core.Entities;

namespace JWTAuthAPI.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {0}", ex.Message);
                await HandleExceptionAsync(ex, httpContext);
            }
        }

        private static async Task HandleExceptionAsync(Exception ex, HttpContext context)
        {
            string message = string.Empty;
            int statusCode;
            switch (ex)
            {
                case ForbiddenAccessException:
                    statusCode = StatusCodes.Status403Forbidden;
                    break;
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case ValidationException:
                case ResourceCreationException:
                case ResourceModificationException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;
                case UnauthenticatedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "Internal Server Error";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = statusCode,
                Message = message
            }
            .ToString());
        }
    }
}
