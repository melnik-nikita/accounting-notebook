using System.Threading.Tasks;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Web.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, ValidationException exception)
        {
            var statusCode = StatusCodes.Status400BadRequest;
            var message = exception?.Message ?? "Bad request";

            switch (exception)
            {
                case InvalidAccountBalanceException accountBalanceException:
                    statusCode = StatusCodes.Status409Conflict;

                    break;
            }

            var result = JsonConvert.SerializeObject(new { Error = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
