using hrms.Domain.Models.Auth;
using hrms.Persistance;
using hrms.Persistance.Entities;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
                return new ServiceResult<User>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "Passwords don't match."
                };
            }
            if (await UserExists(registerDto.Username))
            {
                return new ServiceResult<User>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "Username is registered."
                };
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
                            .SingleOrDefaultAsync(x => x.Username == loginDto.Username.ToLower(), cancellationToken: cancellationToken);
            if (user == null)
            {
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "Wrong username."
                };
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return new ServiceResult<string>()
                    {
                        ErrorOccured = true,
                        ErrorMessage = "Incorrect password."
                    };
                }
            }

            var generateToken = GenerateJwtToken(user, false);
            var generateRefreshToken = GenerateJwtToken(user, true);

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

        public async Task<ServiceResult<string>> UpdateAccessToken(string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(accessToken);
                string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new ServiceResult<string>()
                    {
                        ErrorOccured = true,
                        ErrorMessage = "UserId not found in access token."
                    };
                }

                var getRefreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId.ToString() == userId, cancellationToken);

                if (getRefreshToken == null)
                {
                    return new ServiceResult<string>()
                    {
                        ErrorOccured = true,
                        ErrorMessage = "Refresh token not found."
                    };
                }

                if (DateTime.UtcNow >= getRefreshToken.ExpiryDate)
                {
                    return new ServiceResult<string>()
                    {
                        ErrorOccured = true,
                        ErrorMessage = "Refresh token expired."
                    };
                }

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.ToString() == userId, cancellationToken);

                if (user == null)
                {
                    return new ServiceResult<string>()
                    {
                        ErrorOccured = true,
                        ErrorMessage = $"User with id: {userId} not found."
                    };
                }

                if (!(user.IsActive ?? false))
                {
                    return new ServiceResult<string>()
                    {
                        ErrorOccured = true,
                        ErrorMessage = $"User with id: {userId} is blocked."
                    };
                }

                var generateNewToken = GenerateJwtToken(user, false);

                return new ServiceResult<string>
                {
                    Success = true,
                    Data = generateNewToken.Item1
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = $"Error decoding access token: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResult<string>> ResetPassword(string usernameOrEmail, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail, cancellationToken);
            if (user == null)
            {
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "User not found."
                };
            }
            if (!(user.IsActive ?? false))
            {
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "Your user is blocked. Contact Support to get help."
                };
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "Email not found."
                };
            }

            var secretKey = _configuration["Jwt:PasswordRecoverySecretKey"];

            if (string.IsNullOrEmpty(secretKey))
            {
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = "Secret key not found."
                };
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
                return new ServiceResult<string>()
                {
                    ErrorOccured = true,
                    ErrorMessage = ex.Message
                };
            }

        }

        private (string, DateTime) GenerateJwtToken(User user, bool isRefreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

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

        private string GeneratePasswordResetToken(string userId, string email, string secretKey)
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
