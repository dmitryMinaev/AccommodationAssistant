using AccommodationAssistant.Presentation.Middlewares;
using Microsoft.OpenApi.Models;

namespace AccommodationAssistant.Presentation.Extensions
{
    public static class SwaggerExtension
    {
        private const string _apiName = "AccommodationAssistant API";
        private const string _apiVersion = "v1";

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    _apiVersion,
                    new OpenApiInfo
                    {
                        Title = _apiName,
                        Version = _apiVersion
                    });

                options.AddSecurityDefinition("SecretKey", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = ApiKeyMiddleware.ApiKeyHeader,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "SecretKey"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
