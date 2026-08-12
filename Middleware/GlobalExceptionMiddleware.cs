using EmployeeManagement.Exceptions;
using System.Text.Json;

namespace EmployeeManagement.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch(Exception ex)
            {
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
