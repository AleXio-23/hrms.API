using hrms.Application.Services.UserProfile.CreateUserProfile;
using hrms.Domain.Models.Auth;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ICreateNewProfile _createNewProfileService;

        public ProfileController(ICreateNewProfile createNewProfileService)
        {
            _createNewProfileService = createNewProfileService;
        }


        [HttpPost("CreateProfile")]
        public async Task<ActionResult<ServiceResult<User>>> CreateNewProfile([FromBody] UserProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            var result = await _createNewProfileService.Execute(profileDTO, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
