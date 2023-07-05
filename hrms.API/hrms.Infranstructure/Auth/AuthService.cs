using hrms.Domain.Models.Auth;
using hrms.Persistance;
using hrms.Persistance.Entities;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace hrms.Infranstructure.Auth
{
    public class AuthService : IAuthService
    {

        private readonly HrmsAppDbContext _dbContext;
        private readonly IConfiguration _configuration;


        public AuthService(HrmsAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }


        public async Task<ServiceResult<User>> Register(RegisterDto registerDto, CancellationToken cancellationToken)
        {
            if (registerDto.Password != registerDto.RepeatPassword)
            {
                throw new ArgumentException("Passwords don't match");
            }
            if (await UserExists(registerDto.Username))
            {
                throw new RecordExistsException("Username is registered");
            }

            var hmac = new HMACSHA512();
            var newUser = new User()
            {
                Username = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ServiceResult<User>()
            {
                Success = true,
                Data = newUser
            };
        }

        public async Task<ServiceResult<string>> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                            .SingleOrDefaultAsync(x => x.Username == loginDto.Username.ToLower(), cancellationToken: cancellationToken) ?? throw new ArgumentException("Wrong username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    throw new ArgumentException("Incorrect password");
                }
            }
            var jwtSecret = _configuration["Jwt:Secret"] ?? throw new NotFoundException("Jwt Secret Key not found");

            var generateToken = GenerateJwtToken(user, jwtSecret, false);
            var generateRefreshToken = GenerateJwtToken(user, jwtSecret, true);

            var checkRefreshTokenIfExists = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
            if (checkRefreshTokenIfExists != null)
            {
                checkRefreshTokenIfExists.Token = generateRefreshToken.Item1;
                checkRefreshTokenIfExists.ExpiryDate = generateRefreshToken.Item2;
            }
            else
            {
                var refreshTokenObject = new RefreshToken()
                {
                    UserId = user.Id,
                    Token = generateRefreshToken.Item1,
                    ExpiryDate = generateRefreshToken.Item2

                };
                _dbContext.RefreshTokens.Add(refreshTokenObject);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ServiceResult<string>()
            {
                Success = true,
                Data = generateToken.Item1
            };
        }

        public async Task<ServiceResult<string>> UpdateAccessToken(string? accessToken, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new ArgumentException("Wrong accessToken");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(accessToken);
                string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentException("UserId not found in access token");
                }

                var getRefreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId.ToString() == userId, cancellationToken) ?? throw new NotFoundException("Refresh token not found");

                if (DateTime.UtcNow >= getRefreshToken.ExpiryDate)
                {
                    throw new SecurityTokenExpiredException("Refresh token expired");
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId, cancellationToken) ?? throw new NotFoundException($"User with id: {userId} not found.");

                if (!(user.IsActive ?? false))
                {
                    throw new ValidationException($"User with id: {userId} is blocked.");
                }
                var jwtSecret = _configuration["Jwt:Secret"] ?? throw new NotFoundException("Jwt Secret Key not found");

                var generateNewToken = GenerateJwtToken(user, jwtSecret, false);

                return new ServiceResult<string>
                {
                    Success = true,
                    Data = generateNewToken.Item1
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResult<string>> ResetPassword(string usernameOrEmail, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail, cancellationToken) ?? throw new NotFoundException("User not found");
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

        private static (string, DateTime) GenerateJwtToken(User user, string jwtSecret, bool isRefreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret ?? "");

            var expireDate = isRefreshToken ? DateTime.UtcNow.AddHours(24) : DateTime.UtcNow.AddMinutes(5);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new System.Security.Claims.Claim("Id", user.Id.ToString()),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),

                Expires = expireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return (jwtTokenHandler.WriteToken(token), expireDate);
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

        private async Task<bool> UserExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
    }
}
