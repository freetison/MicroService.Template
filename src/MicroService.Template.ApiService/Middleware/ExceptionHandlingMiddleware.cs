namespace MicroService.Template.ApiService.Middleware
{
    using MicroService.Template.Core.Exceptions;

    using System.Net;

    using ValidationException = Core.Exceptions.ValidationException;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation failed: {Message}", ex.Message);
                context.Response.StatusCode = ex.ErrorCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message,
                    validationErrors = ex.Errors
                });
            }
            catch (CustomException ex)
            {
                _logger.LogWarning(ex, "Handled exception: {Message}", ex.Message);
                context.Response.StatusCode = ex.ErrorCode;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "Internal Server Error" });
            }
        }
    }

}
