using hrms.Application.Services.Accounting;
using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.GetBackFromBreak;
using hrms.Application.Services.Accounting.LogLateFromBreak.AddLogLateFromBreak;
using hrms.Application.Services.Accounting.LogLateFromBreak.GetLogLateFromBreak;
using hrms.Application.Services.Accounting.LogLateFromBreak.GetLogsLateFromBreak;
using hrms.Application.Services.Accounting.StartAccounting;
using hrms.Application.Services.Accounting.TakeBreak;
using hrms.Application.Services.Accounting.WorkOnLateLog.AddWorkOnLateLog;
using hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLog;
using hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLogs;
using hrms.Application.Services.Configuration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.UpdateNumberTypesConfiguration;
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
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.AddOrUpdateCompanyHolidays;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.DeleteCompanyHoliday;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHoliday;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeType;
using hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeTypes;
using hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayType;
using hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayTypes;
using hrms.Application.Services.Documents;
using hrms.Application.Services.Documents.DocumentsUpload.UploadDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument;
using hrms.Application.Services.Documents.DocumentTypes.AddOrUpdateDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.DeleteDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentTypes;
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
using hrms.Application.Services.User.UsersWorkSchedule.AddOrUpdateUsersWorkSchedule;
using hrms.Application.Services.User.UsersWorkSchedule.DeleteUsersWorkSchedule;
using hrms.Application.Services.UserProfile;
using hrms.Application.Services.Vacation;
using hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave;
using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;
using hrms.Application.Services.Vacation.QuartersCounts;
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
            services.AddScoped<IAddOrUpdateUsersWorkScheduleService, AddOrUpdateUsersWorkScheduleService>();
            services.AddScoped<IDeleteUsersWorkScheduleService, DeleteUsersWorkScheduleService>();

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
            services.AddScoped<IAddOrUpdateCompanyHolidaysService, AddOrUpdateCompanyHolidaysService>();
            services.AddScoped<IDeleteCompanyHolidayService, DeleteCompanyHolidayService>();
            services.AddScoped<IGetCompanyHolidayService, GetCompanyHolidayService>();
            services.AddScoped<IGetCompanyHolidaysService, GetCompanyHolidaysService>();
            services.AddScoped<IGetHolidayRangeTypeService, GetHolidayRangeTypeService>();
            services.AddScoped<IGetHolidayRangeTypesService, GetHolidayRangeTypesService>();
            services.AddScoped<IGetHolidayTypesService, GetHolidayTypesService>();
            services.AddScoped<IGetHolidayTypeService, GetHolidayTypeService>();

            services.AddScoped<IAccountingFacade, AccountingFacade>();
            services.AddScoped<IStartAccountingService, StartAccountingService>();
            services.AddScoped<IFinishAccountingService, FinishAccountingService>();
            services.AddScoped<ITakeBreakService, TakeBreakService>();
            services.AddScoped<IGetBackFromBreakService, GetBackFromBreakService>();
            services.AddScoped<IAddWorkOnLateLogService, AddWorkOnLateLogService>();
            services.AddScoped<IGetWorkOnLateLogService, GetWorkOnLateLogService>();
            services.AddScoped<IGetWorkOnLateLogsService, GetWorkOnLateLogsService>();
            services.AddScoped<IAddLogLateFromBreakService, AddLogLateFromBreakService>();
            services.AddScoped<IGetLogLateFromBreakService, GetLogLateFromBreakService>();
            services.AddScoped<IGetLogsLateFromBreakService, GetLogsLateFromBreakService>();

            services.AddScoped<IConfigurationFacade, ConfigurationFacade>();
            services.AddScoped<IAddOrUpdateNumberTypesConfigurationService, AddOrUpdateNumberTypesConfigurationService>();
            services.AddScoped<IDeleteNumberTypesConfigurationService, DeleteNumberTypesConfigurationService>();
            services.AddScoped<IGetNumberTypesConfigurationService, GetNumberTypesConfigurationService>();
            services.AddScoped<IGetNumberTypesConfigurationsService, GetNumberTypesConfigurationsService>();

            services.AddScoped<IDocumentsFacade, DocumentsFacade>();
            services.AddScoped<IAddOrUpdateDocumentTypeService, AddOrUpdateDocumentTypeService>();
            services.AddScoped<IDeleteDocumentTypeService, DeleteDocumentTypeService>();
            services.AddScoped<IGetDocumentTypeService, GetDocumentTypeService>();
            services.AddScoped<IGetDocumentTypesService, GetDocumentTypesService>();
            services.AddScoped<IAddUploadedDocumentService, AddUploadedDocumentService>();
            services.AddScoped<IAddUserUploadedDocumentService, AddUserUploadedDocumentService>();
            services.AddScoped<IUploadDocumentService, UploadDocumentService>();

            services.AddScoped<IVacationsFacade, VacationsFacade>();
            services.AddScoped<IAddOrUpdatePayedLeaveService, AddOrUpdatePayedLeaveService>();
            services.AddScoped<IGetCurrentActivePayedLeavesService, GetCurrentActivePayedLeavesService>();
            services.AddScoped<IQuartersCountsService, QuartersCountsService>();

            return services;
        }
    }
}
