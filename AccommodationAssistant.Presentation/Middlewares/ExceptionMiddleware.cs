using AccommodationAssistant.Domain.Exceptions;
using AccommodationAssistant.Presentation.Models;
using System.Net.Mime;

namespace AccommodationAssistant.Presentation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch(ApiKeyException ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status401Unauthorized, ex);
            }
            catch (ContractException ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, int statusCode, Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(new ExceptionDetails
            {
                StatusCode = statusCode,
                Message = ex.Message,
                StackTrace = ex.StackTrace
            });
        }
    }
}
