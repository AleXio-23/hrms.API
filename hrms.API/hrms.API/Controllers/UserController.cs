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
    }
}
