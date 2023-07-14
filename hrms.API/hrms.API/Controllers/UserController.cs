using hrms.Application.Services.UserProfile;
using hrms.Domain.Models.Shared;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
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
            var result = await _userProfileFacade.CreateNewProfile.Execute(profileDTO, cancellationToken);
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
            var result = await _userProfileFacade.UpdateUserProfileService.Execute(profileDTO, cancellationToken);
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
            var result = await _userProfileFacade.AddOrUpdateUserJobPositionService.Execute(userJobPositionDTO, cancellationToken);
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
            var result = await _userProfileFacade.DeleteUserJobPositionService.Execute(userId.UserId, cancellationToken);
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
            var result = await _userProfileFacade.GetUserJobPositionService.Execute(userId, cancellationToken);
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
        [HttpPost("AddOrUpdateRoles")]
        public async Task<ActionResult<ServiceResult<RoleDTO>>> AddOrUpdateRoles([FromBody] RoleDTO roleDTO, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateRolesService.Execute(roleDTO, cancellationToken);
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
            var result = await _userProfileFacade.DeleteRoleService.Execute(roleId.Id, cancellationToken);
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
            var result = await _userProfileFacade.GetRoleService.Execute(roleId, cancellationToken);
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
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> GetRoles([FromQuery] RolesFilter filter, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.GetRolesService.Execute(filter, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        #endregion

        #region UserRole Cruds
        [HttpPost("AddOrUpdateUserRole")]
        public async Task<ActionResult<ServiceResult<UserJobPositionDTO>>> AddOrUpdateUserRole([FromBody] AddOrUpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileFacade.AddOrUpdateUserRoleService.Execute(request, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        #endregion
    }
}
