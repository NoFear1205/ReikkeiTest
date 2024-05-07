using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Test.Application.Common.Behaviours;
using Test.Application.Common.Models;

namespace Test.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });
            return services;
        }
    }
}
