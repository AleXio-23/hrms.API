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
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedule;
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules;

namespace hrms.Application.Services.UserProfile
{
    public interface IUserProfileFacade
    {
        ICreateNewProfileService CreateNewProfile { get; }
        IUpdateUserProfileService UpdateUserProfileService { get; }
        IAddOrUpdateUserJobPositionService AddOrUpdateUserJobPositionService { get; }
        IDeleteUserJobPositionService DeleteUserJobPositionService { get; }
        IGetUserJobPositionService GetUserJobPositionService { get; }
        IAddOrUpdateRolesService AddOrUpdateRolesService { get; }
        IDeleteRoleService DeleteRoleService { get; }
        IGetRoleService GetRoleService { get; }
        IGetRolesService GetRolesService { get; }
        IAddOrUpdateUserRoleService AddOrUpdateUserRoleService { get; }
        IAddOrUpdateRoleClaimsService AddOrUpdateRoleClaimsService { get; }
        IDeleteRoleClaimsService DeleteRoleClaimsService { get; }
        IGetRoleClaimService GetRoleClaimService { get; }
        IGetRoleClaimsService GetRoleClaimsService { get; }
        IGetUserService GetUserService { get; }
        IUpdateUserService UpdateUserService { get; }
        IAddOrUpdateUsersWorkScheduleService AddOrUpdateUsersWorkScheduleService { get; }
        IDeleteUsersWorkScheduleService DeleteUsersWorkScheduleService { get; }
        IGetUsersWorkScheduleService GetUsersWorkScheduleService { get; }
        IGetUsersWorkSchedulesService GetUsersWorkSchedulesService { get; }

    }
}
