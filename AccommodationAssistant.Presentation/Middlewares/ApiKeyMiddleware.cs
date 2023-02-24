using AccommodationAssistant.Domain.Exceptions;
using AccommodationAssistant.Presentation.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace AccommodationAssistant.Presentation.Middlewares
{
    public class ApiKeyMiddleware
    {
        public const string ApiKeyHeader = "X-SECRET-KEY";

        private readonly RequestDelegate _next;
        private readonly ApiConfiguration _apiConfiguration;

        public ApiKeyMiddleware(RequestDelegate next, IOptions<ApiConfiguration> apiConfiguration)
        {
            _next = next;
            _apiConfiguration = apiConfiguration.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out StringValues value))
            {
                throw new ApiKeyException("Key wasn't provided");
            }

            if (!_apiConfiguration.SecretKey.Equals(value))
            {
                throw new ApiKeyException("Unauthorized client");
            }

            await _next(context);
        }
    }
}
