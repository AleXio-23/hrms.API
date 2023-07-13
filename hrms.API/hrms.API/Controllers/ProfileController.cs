using hrms.Application.Services.UserProfile;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileFacade _userProfileFacade;

        public ProfileController(IUserProfileFacade userProfileFacade)
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
    }
}
