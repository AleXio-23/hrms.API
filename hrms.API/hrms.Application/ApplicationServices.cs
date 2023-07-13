using hrms.Application.Services.Dictionaries;
using hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender;
using hrms.Application.Services.Dictionaries.Gender.DeleteGender;
using hrms.Application.Services.Dictionaries.Gender.GetGender;
using hrms.Application.Services.Dictionaries.Gender.GetGenders;
using hrms.Application.Services.UserProfile;
using hrms.Application.Services.UserProfile.CreateUserProfile;
using hrms.Application.Services.UserProfile.UpdateUserProfile;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateNewProfileService, CreateNewProfileService>();
            services.AddScoped<IUpdateUserProfileService, UpdateUserProfileService>();
            services.AddScoped<IUserProfileFacade, UserProfileFacade>();


            services.AddScoped<IGetGenderService, GetGenderService>();
            services.AddScoped<IAddOrUpdateGenderService, AddOrUpdateGenderService>();
            services.AddScoped<IGetGendersService, GetGendersService>();
            services.AddScoped<IDeleteGenerService, DeleteGenerService>();
            services.AddScoped<IDictionaryiFacade, DictionaryiFacade>();

            return services;
        }
    }
}
