using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentation.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await HandleFluentValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleFluentValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;


            var errors = new Dictionary<string, string[]>();

            foreach (var error in exception.Errors)
            {
                errors[error.PropertyName] = new[] { error.ErrorMessage };
            }

            var errorDetails = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Request failed!",
                Errors = new List<string>() { "sfdfgdfdf"}
            };

            return context.Response.WriteAsJsonAsync(errorDetails);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorDetails = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Request failed!",
                Errors = new List<object>() { exception.Message }// Optional: You can include more details if necessary
            };

            return context.Response.WriteAsJsonAsync(errorDetails);
        }

    }
}
