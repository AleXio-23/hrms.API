using hrms.Infranstructure.Auth;
using hrms.Infranstructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Infranstructure
{
    public static class InfranstructureServices
    {

        public static IServiceCollection RegisterIfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogger, Logger>();

            return services;
        }

    }
}
