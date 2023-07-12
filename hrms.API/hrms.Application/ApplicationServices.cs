using hrms.Application.Services.UserProfile.CreateUserProfile;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateNewProfile, CreateNewProfile>();


            return services;
        }
    }
}
