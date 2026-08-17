using EmployeeManagement.Exceptions;
using System.Text.Json;

namespace EmployeeManagement.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next ,ILogger<GlobalExceptionMiddleware> logger)
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

            catch(Exception ex)
            {
                _logger.LogError(ex, "UnhandledException occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async  Task HandleExceptionAsync(HttpContext context ,Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;

            switch(exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;

                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;

                case ConflictException:
                    statusCode = StatusCodes.Status409Conflict;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;

            }
            context.Response.StatusCode = statusCode;

            var response = new
            {
                StatusCodes = statusCode,
                Message = exception.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }

    }
}
