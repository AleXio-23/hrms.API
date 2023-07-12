using hrms.Domain.Models.Auth;
using hrms.Infranstructure.Auth.AccessToken.UpdateAccessToken;
using hrms.Infranstructure.Auth.LogIn;
using hrms.Infranstructure.Auth.LogOut;
using hrms.Infranstructure.Auth.Register;
using hrms.Infranstructure.Auth.ResetPassword;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILogInService _loginService;
        private readonly IUpdateAccessTokenService _updateAccessTokenService;
        private readonly ILogOutService _logOutService;
        private readonly IResetPasswordService _resetPasswordService;

        public AuthController(IRegisterService registerService,
                                ILogInService loginService,
                                IUpdateAccessTokenService updateAccessTokenService,
                                ILogOutService logOutService,
                                IResetPasswordService resetPasswordService)
        {
            _registerService = registerService;
            _loginService = loginService;
            _updateAccessTokenService = updateAccessTokenService;
            _logOutService = logOutService;
            _resetPasswordService = resetPasswordService;
        }



        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="registerDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResult<User>>> RegisterNewUser([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var result = await _registerService.Execute(registerDto, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="loginDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("signIn")]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> SignIn([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var result = await _loginService.Exeute(loginDto, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Access token update
        /// </summary>
        /// <param name="updateAccessTokenRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("UpdateAccessToken")]
        public async Task<ActionResult<ServiceResult<string>>> UpdateAccessToken([FromBody] UpdateAccessTokenRequest updateAccessTokenRequest, CancellationToken cancellationToken)
        {
            var result = await _updateAccessTokenService.Execute(updateAccessTokenRequest.AccessToken, cancellationToken);

            if (result.ErrorOccured)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }


        /// <summary>
        /// Sign out
        /// </summary>
        /// <param name="updateAccessTokenRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("SignOut")]
        public async Task<ActionResult<ServiceResult<bool>>> SignOut([FromBody] UpdateAccessTokenRequest updateAccessTokenRequest, CancellationToken cancellationToken)
        {
            var result = await _logOutService.Execute(updateAccessTokenRequest.AccessToken, cancellationToken);

            if (result.ErrorOccured)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Recover forgotten password
        /// </summary>
        /// <param name="userNameOrEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ServiceResult<string>>> ResetPassword([FromBody] string userNameOrEmail, CancellationToken cancellationToken)
        {
            var result = await _resetPasswordService.Execute(userNameOrEmail, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
