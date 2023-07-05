using hrms.Infranstructure.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Infranstructure
{
    public static class InfranstructureServices
    {

        public static IServiceCollection RegisterIfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

    }
}
