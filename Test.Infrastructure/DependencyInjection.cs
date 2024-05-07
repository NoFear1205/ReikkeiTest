using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Test.Application.Interfaces;
using Test.Domain.Constants;
using Test.Infrastructure.Data;
using Test.Infrastructure.Data.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Test.Infrastructure.AuthenticationHandler;

namespace Test.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConfigKey.DatabaseConnectString);

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
                options.UseSnakeCaseNamingConvention();

            });
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddBearerToken().AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"])),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JwtConfig:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtConfig:Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                }).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(AuthorizationSchemas.ApiKey, null); ;
            services.AddAuthorizationBuilder();

            services.AddSingleton(TimeProvider.System);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ApiKey, policy =>
                        policy.AddAuthenticationSchemes(AuthorizationSchemas.ApiKey)
                         .RequireAuthenticatedUser());
            });

            return services;
        }
    }
}
