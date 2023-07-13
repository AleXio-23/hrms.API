using hrms.Application.Services.Dictionaries;
using hrms.Application.Services.Dictionaries.Departments.AddOrUpdateDepartment;
using hrms.Application.Services.Dictionaries.Departments.DeleteDepartment;
using hrms.Application.Services.Dictionaries.Departments.GetDepartment;
using hrms.Application.Services.Dictionaries.Departments.GetDepartments;
using hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender;
using hrms.Application.Services.Dictionaries.Gender.DeleteGender;
using hrms.Application.Services.Dictionaries.Gender.GetGender;
using hrms.Application.Services.Dictionaries.Gender.GetGenders;
using hrms.Application.Services.Dictionaries.JobPositions.AddOrUpdateJobPosition;
using hrms.Application.Services.Dictionaries.JobPositions.DeleteJobPosition;
using hrms.Application.Services.Dictionaries.JobPositions.GetJobPosition;
using hrms.Application.Services.Dictionaries.JobPositions.GetJobPositions;
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

            services.AddScoped<IGetDepartmentsService, GetDepartmentsService>();
            services.AddScoped<IGetDepartmentService, GetDepartmentService>();
            services.AddScoped<IDeleteDepartmentService, DeleteDepartmentService>();
            services.AddScoped<IAddOrUpdateDepartmentService, AddOrUpdateDepartmentService>();

            services.AddScoped<IAddOrUpdateJobPositionService, AddOrUpdateJobPositionService>();
            services.AddScoped<IDeleteJobPositionService, DeleteJobPositionService>();
            services.AddScoped<IGetJobPositionService, GetJobPositionService>();
            services.AddScoped<IGetJobPositionsService, GetJobPositionsService>();

            services.AddScoped<IDictionaryiFacade, DictionaryiFacade>();

            return services;
        }
    }
}
