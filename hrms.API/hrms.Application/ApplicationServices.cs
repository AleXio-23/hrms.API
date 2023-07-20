using hrms.Application.Services.Accounting;
using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.StartAccounting;
using hrms.Application.Services.Accounting.TakeBreak;
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
using hrms.Application.Services.User.GetUser;
using hrms.Application.Services.User.RoleClaims.AddOrUpdateRoleClaims;
using hrms.Application.Services.User.RoleClaims.DeleteRoleClaims;
using hrms.Application.Services.User.RoleClaims.GetRoleClaim;
using hrms.Application.Services.User.RoleClaims.GetRoleClaims;
using hrms.Application.Services.User.UpdateUser;
using hrms.Application.Services.User.UserJobPosition.AddOrUpdateUserJobPosition;
using hrms.Application.Services.User.UserJobPosition.DeleteUserJobPosition;
using hrms.Application.Services.User.UserJobPosition.GetUserJobPosition;
using hrms.Application.Services.User.UserProfile.CreateUserProfile;
using hrms.Application.Services.User.UserProfile.UpdateUserProfile;
using hrms.Application.Services.User.UserRoles.Roles.AddOrUpdateRoles;
using hrms.Application.Services.User.UserRoles.Roles.DeleteRole;
using hrms.Application.Services.User.UserRoles.Roles.GetRole;
using hrms.Application.Services.User.UserRoles.Roles.GetRoles;
using hrms.Application.Services.User.UserRoles.UserRoles.AddOrUpdateUserRole;
using hrms.Application.Services.UserProfile;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserProfileFacade, UserProfileFacade>();
            services.AddScoped<ICreateNewProfileService, CreateNewProfileService>();
            services.AddScoped<IUpdateUserProfileService, UpdateUserProfileService>();
            services.AddScoped<IAddOrUpdateUserJobPositionService, AddOrUpdateUserJobPositionService>();
            services.AddScoped<IDeleteUserJobPositionService, DeleteUserJobPositionService>();
            services.AddScoped<IGetUserJobPositionService, GetUserJobPositionService>();
            services.AddScoped<IAddOrUpdateRolesService, AddOrUpdateRolesService>();
            services.AddScoped<IDeleteRoleService, DeleteRoleService>();
            services.AddScoped<IGetRoleService, GetRoleService>();
            services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<IAddOrUpdateUserRoleService, AddOrUpdateUserRoleService>();
            services.AddScoped<IGetRoleClaimsService, GetRoleClaimsService>();
            services.AddScoped<IGetRoleClaimService, GetRoleClaimService>();
            services.AddScoped<IDeleteRoleClaimsService, DeleteRoleClaimsService>();
            services.AddScoped<IAddOrUpdateRoleClaimsService, AddOrUpdateRoleClaimsService>();
            services.AddScoped<IGetUserService, GetUserService>();
            services.AddScoped<IUpdateUserService, UpdateUserService>();


            services.AddScoped<IDictionaryiFacade, DictionaryiFacade>();
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

            services.AddScoped<IAccountingFacade, AccountingFacade>();
            services.AddScoped<IStartAccountingService, StartAccountingService>();
            services.AddScoped<IFinishAccountingService, FinishAccountingService>();
            services.AddScoped<ITakeBreakService, TakeBreakService>();

            return services;
        }
    }
}
