﻿using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace hrms.Infranstructure.Auth.ResetPassword
{
    public class ResetPasswordService: IResetPasswordService
    {
        private readonly IRepository<User> _userRepository; 
        private readonly IConfiguration _configuration;

        public ResetPasswordService(IRepository<User> userRepository, 
                            IConfiguration configuration)
        {
            _userRepository = userRepository; 
            _configuration = configuration;
        }
        public async Task<ServiceResult<string>> Execute(string usernameOrEmail, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail, cancellationToken) ?? throw new NotFoundException("User not found");
            if (!(user.IsActive ?? false))
            {
                throw new ValidationException("Your user is blocked. Contact Support to get help");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new NotFoundException("Email not found");
            }

            var secretKey = _configuration["Jwt:PasswordRecoverySecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new NotFoundException("Secret key not found");
            }

            var generatePasswordResetToken = GeneratePasswordResetToken(user.Id.ToString(), user.Email, secretKey);

            try
            {
                //todo
                //Send this token on mail
                return new ServiceResult<string>()
                {
                    Success = true,
                    Data = "Message Sent"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private static string GeneratePasswordResetToken(string userId, string email, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, userId),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24), // Set the token expiration time
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
