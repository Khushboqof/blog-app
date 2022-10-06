using Microsoft.OpenApi.Models;

namespace BlogApp.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwaggerAuthorize(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization headerusing the Bearer scheme. " +
                        "Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
