using hrms.Application.Services.User;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Shared;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hrms.API.Controllers
{
    /// <summary>
    /// User services endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileFacade _userProfileFacade;

        public UserController(IUserProfileFacade userProfileFacade)
        {
            _userProfileFacade = userProfileFacade;
        }

        /// <summary>
        /// Create new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CreateProfile")]
        public async Task<ActionResult<ServiceResult<User>>> CreateNewProfile([FromBody] UserProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.CreateNewProfile.Execute(profileDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Update user profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("UpdateProfile")]
        public async Task<ActionResult<ServiceResult<User>>> UpdateUserProfile([FromBody] UserProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.UpdateUserProfileService.Execute(profileDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        #region User JobPosition Cruds
        /// <summary>
        /// Add or update user job position record
        /// </summary>
        /// <param name="userJobPositionDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateUserJobPosition")]
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> AddOrUpdateUserJobPosition([FromBody] UserJobPositionDTO userJobPositionDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateUserJobPositionService.Execute(userJobPositionDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete uer job position record
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUserJobPosition")]
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> DeleteUserJobPosition([FromBody] UserIdRequest userId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.DeleteUserJobPositionService.Execute(userId.UserId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get user job position based on user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUserJobPosition")]
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> GetUserJobPosition([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetUserJobPositionService.Execute(userId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion

        #region Roles Cruds
        /// <summary>
        /// Add or update existing role
        /// </summary>
        /// <param name="roleDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateRole")]
        public async Task<ActionResult<ServiceResult<RoleDTO>>> AddOrUpdateRoles([FromBody] RoleDTO roleDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateRolesService.Execute(roleDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete role (Removes only if it's not assigned to any user)
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRole")]
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> DeleteRole([FromBody] IdRequest roleId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.DeleteRoleService.Execute(roleId.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get single role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetRole")]
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> GetRole([FromQuery] int roleId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetRoleService.Execute(roleId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get roles list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<ActionResult<ServiceResult<List<UserJobPositionDTO>>>> GetRoles([FromQuery] RolesFilter filter, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetRolesService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        #endregion

        #region UserRole Cruds


        /// <summary>
        /// Add new role for user
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateUserRole")]
        public async Task<ActionResult<ServiceResult<bool>>> AddOrUpdateUserRole([FromBody] AddOrUpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateUserRoleService.Execute(request, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion

        #region RoleClaims Cruds

        /// <summary>
        /// Assing new claim to role
        /// </summary>
        /// <param name="roleClaimsDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateRoleClaims")]
        public async Task<ActionResult<ServiceResult<RoleClaimsDTO>>> AddOrUpdateRoleClaims([FromBody] RoleClaimsDTO roleClaimsDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateRoleClaimsService.Execute(roleClaimsDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete claim for role
        /// </summary>
        /// <param name="roleClaimsDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRoleClaim")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteRoleClaim([FromBody] RoleClaimsDTO roleClaimsDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.DeleteRoleClaimsService.Execute(roleClaimsDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get single claim for role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="claimId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetRoleClaim")]
        public async Task<ActionResult<ServiceResult<ClaimsDTO>>> GetRoleClaim([FromQuery] int roleId, [FromQuery] int claimId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetRoleClaimService.Execute(roleId, claimId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Get role assigned claims
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetRoleClaims")]
        public async Task<ActionResult<ServiceResult<List<ClaimsDTO>>>> GetRoleClaims([FromQuery] int roleId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetRoleClaimsService.Execute(roleId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion


        #region GetSingleUser By Id

        /// <summary>
        /// Get user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        public async Task<ActionResult<ServiceResult<UserDTO>>> GetUser([FromQuery] int userId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetUserService.Execute(userId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// update user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserFromProfile")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<UserDTO>>> UpdateUser([FromBody] UserDTO userDTO, CancellationToken cancellationToken)
        {
            string userId = User.FindFirstValue("Id");
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("You need to authorize to execute this request");
            }
            else
            {
                if (userId != userDTO.Id.ToString())
                {
                    throw new ArgumentException("You cant update another persons profile information");
                }
            }

            var result = await _userProfileFacade.UpdateUserService.Execute(userDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion

        #region AddOrUpdateUsersWorkScheduleService CRUDs - Add for each user, for each week day if its work day for user and from what time to time he/she works

        /// <summary>
        ///  Get user work schedule based on user work schedule id
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUsersWorkSchedule")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> GetUsersWorkSchedule([FromQuery] int scheduleId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetUsersWorkScheduleService.Execute(scheduleId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        ///  Get user work schedule based on user id and week day
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="weekday"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUsersWorkScheduleWithUserIdAndWeekDay")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> GetUsersWorkScheduleWithUserIdAndWeekDay([FromQuery] int userId, [FromQuery] string weekday, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetUsersWorkScheduleService.Execute(userId, weekday, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get user work schedule based on user id and week day Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="weekdayId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUsersWorkScheduleWithUserIdAndWeekDayId")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> GetUsersWorkScheduleWithUserIdAndWeekDayId([FromQuery] int userId, [FromQuery] int weekdayId, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetUsersWorkScheduleService.Execute(userId, weekdayId, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get user work schedules with filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetUsersWorkSchedules")]
        public async Task<ActionResult<ServiceResult<List<GenderDTO>>>> GetUsersWorkSchedules([FromQuery] UsersWorkScheduleFilter filter, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetUsersWorkSchedulesService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add new or update existng User work scedule
        /// </summary>
        /// <param name="usersWorkScheduleDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateUsersWorkSchedule")]
        public async Task<ActionResult<ServiceResult<GenderDTO>>> AddOrUpdateGender([FromBody] UsersWorkScheduleDTO usersWorkScheduleDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateUsersWorkScheduleService.Execute(usersWorkScheduleDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Delete user work schedule 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUsersWorkSchedule")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteUsersWorkSchedule([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.DeleteUsersWorkScheduleService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion
    }
}
