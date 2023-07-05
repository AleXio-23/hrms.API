using hrms.Domain.Models.Auth;
using hrms.Infranstructure.Auth;
using hrms.Persistance.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterNewUser([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var result = await _authService.Register(registerDto, cancellationToken);
            return Ok(result);
        }


        [HttpPost("signIn")]
        public async Task<ActionResult<User>> SignIn([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var result = await _authService.Login(loginDto, cancellationToken);
            return Ok(result);
        }
    }
}
