﻿using hrms.Domain.Models.Auth;
using hrms.Infranstructure.Auth;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
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


        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="registerDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResult<User>>> RegisterNewUser([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var result = await _authService.Register(registerDto, cancellationToken);
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
        public async Task<ActionResult<ServiceResult<string>>> SignIn([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var result = await _authService.Login(loginDto, cancellationToken);
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
            var result = await _authService.UpdateAccessToken(updateAccessTokenRequest.AccessToken, cancellationToken);

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
            var result = await _authService.ResetPassword(userNameOrEmail, cancellationToken);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
