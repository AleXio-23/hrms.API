using hrms.Infranstructure.Auth;
using hrms.Infranstructure.Auth.AccessToken.UpdateAccessToken;
using hrms.Infranstructure.Auth.LogIn;
using hrms.Infranstructure.Auth.LogOut;
using hrms.Infranstructure.Auth.Register;
using hrms.Infranstructure.Auth.ResetPassword;
using hrms.Infranstructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Infranstructure
{
    public static class InfranstructureServices
    { 
        public static IServiceCollection RegisterIfrastructureServices(this IServiceCollection services)
        { 
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ILogInService, LogInService>();
            services.AddScoped<IUpdateAccessTokenService, UpdateAccessTokenService>();
            services.AddScoped<ILogOutService, LogOutService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IAuthenticationFacade, AuthenticationFacade>();

            services.AddScoped<ILogger, Logger>();

            return services;
        } 
    }
}
