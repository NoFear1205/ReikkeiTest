using Microsoft.AspNetCore.Mvc;
using Test.API.Infrastructure;
using Test.API.Services;
using Test.Application.Interfaces;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Test.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            services.AddExceptionHandler<CustomExceptionHandler>();

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument((configure, sp) =>
            {
                configure.Title = "Dashboard API";

                // Add JWT
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            
            return services;
        }
    }
}
