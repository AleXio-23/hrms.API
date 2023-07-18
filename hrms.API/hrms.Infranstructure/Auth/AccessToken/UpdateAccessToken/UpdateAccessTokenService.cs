using hrms.Domain.Models.Auth;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hrms.Infranstructure.Auth.AccessToken.UpdateAccessToken
{
    public class UpdateAccessTokenService : IUpdateAccessTokenService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IConfiguration _configuration;

        public UpdateAccessTokenService(IRepository<User> userRepository,
                            IRepository<RefreshToken> refreshTokenRepository,
                            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResult<string>> Execute(string? accessToken, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new ArgumentException("Wrong accessToken");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(accessToken);
                string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentException("UserId not found in access token");
                }

                var getRefreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.UserId.ToString() == userId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException("Refresh token not found");

                if (DateTime.UtcNow >= getRefreshToken.ExpiryDate)
                {
                    throw new SecurityTokenExpiredException("Refresh token expired");
                }

                var user = await _userRepository.FirstOrDefaultAsync(x => x.Id.ToString() == userId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"User with id: {userId} not found.");

                if (!(user.IsActive ?? false))
                {
                    throw new ValidationException($"User with id: {userId} is blocked.");
                }
                var jwtSecret = _configuration["Jwt:Secret"] ?? throw new NotFoundException("Jwt Secret Key not found");

                var generateNewToken = GenerateJwtToken(user, jwtSecret, false);


                return ServiceResult<string>.SuccessResult(generateNewToken.Item1);

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
    }
}
