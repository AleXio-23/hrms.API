using hrms.Application.Services.User.AddNewUser;
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

namespace hrms.Application.Services.User
{
    public class UserProfileFacade : IUserProfileFacade
    {
        public UserProfileFacade(ICreateNewProfileService createNewProfile, IUpdateUserProfileService updateUserProfileService, IAddOrUpdateUserJobPositionService addOrUpdateUserJobPositionService, IDeleteUserJobPositionService deleteUserJobPositionService, IGetUserJobPositionService getUserJobPositionService, IAddOrUpdateRolesService addOrUpdateRolesService, IDeleteRoleService deleteRoleService, IGetRoleService getRoleService, IGetRolesService getRolesService, IAddOrUpdateUserRoleService addOrUpdateUserRoleService, IAddOrUpdateRoleClaimsService addOrUpdateRoleClaimsService, IDeleteRoleClaimsService deleteRoleClaimsService, IGetRoleClaimService getRoleClaimService, IGetRoleClaimsService getRoleClaimsService, IGetUserService getUserService, IUpdateUserService updateUserService, IAddOrUpdateUsersWorkScheduleService addOrUpdateUsersWorkScheduleService, IDeleteUsersWorkScheduleService deleteUsersWorkScheduleService, IGetUsersWorkScheduleService getUsersWorkScheduleService, IGetUsersWorkSchedulesService getUsersWorkSchedulesService, IAddNewUserService addNewUserService)
        {
            CreateNewProfile = createNewProfile;
            UpdateUserProfileService = updateUserProfileService;
            AddOrUpdateUserJobPositionService = addOrUpdateUserJobPositionService;
            DeleteUserJobPositionService = deleteUserJobPositionService;
            GetUserJobPositionService = getUserJobPositionService;
            AddOrUpdateRolesService = addOrUpdateRolesService;
            DeleteRoleService = deleteRoleService;
            GetRoleService = getRoleService;
            GetRolesService = getRolesService;
            AddOrUpdateUserRoleService = addOrUpdateUserRoleService;
            AddOrUpdateRoleClaimsService = addOrUpdateRoleClaimsService;
            DeleteRoleClaimsService = deleteRoleClaimsService;
            GetRoleClaimService = getRoleClaimService;
            GetRoleClaimsService = getRoleClaimsService;
            GetUserService = getUserService;
            UpdateUserService = updateUserService;
            AddOrUpdateUsersWorkScheduleService = addOrUpdateUsersWorkScheduleService;
            DeleteUsersWorkScheduleService = deleteUsersWorkScheduleService;
            GetUsersWorkScheduleService = getUsersWorkScheduleService;
            GetUsersWorkSchedulesService = getUsersWorkSchedulesService;
            AddNewUserService = addNewUserService;
        }

        public ICreateNewProfileService CreateNewProfile { get; }

        public IUpdateUserProfileService UpdateUserProfileService { get; }

        public IAddOrUpdateUserJobPositionService AddOrUpdateUserJobPositionService { get; }

        public IDeleteUserJobPositionService DeleteUserJobPositionService { get; }

        public IGetUserJobPositionService GetUserJobPositionService { get; }

        public IAddOrUpdateRolesService AddOrUpdateRolesService { get; }

        public IDeleteRoleService DeleteRoleService { get; }

        public IGetRoleService GetRoleService { get; }

        public IGetRolesService GetRolesService { get; }

        public IAddOrUpdateUserRoleService AddOrUpdateUserRoleService { get; }

        public IAddOrUpdateRoleClaimsService AddOrUpdateRoleClaimsService { get; }

        public IDeleteRoleClaimsService DeleteRoleClaimsService { get; }

        public IGetRoleClaimService GetRoleClaimService { get; }

        public IGetRoleClaimsService GetRoleClaimsService { get; }

        public IGetUserService GetUserService { get; }

        public IUpdateUserService UpdateUserService { get; }

        public IAddOrUpdateUsersWorkScheduleService AddOrUpdateUsersWorkScheduleService { get; }

        public IDeleteUsersWorkScheduleService DeleteUsersWorkScheduleService { get; }

        public IGetUsersWorkScheduleService GetUsersWorkScheduleService { get; }

        public IGetUsersWorkSchedulesService GetUsersWorkSchedulesService { get; }

        public IAddNewUserService AddNewUserService { get; }
    }
}
