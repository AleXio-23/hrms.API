using hrms.Domain.Models.Auth;
using hrms.Infranstructure.Services.UserActionLogger;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace hrms.Infranstructure.Auth.LogIn
{
    public class LogInService : ILogInService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRepository<VwUserSignInResponse> _vwUserSignInResponseRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserActionLoggerService _userActionLogger;

        public LogInService(IRepository<User> userRepository, IRepository<RefreshToken> refreshTokenRepository, IRepository<VwUserSignInResponse> vwUserSignInResponseRepository, IConfiguration configuration, IUserActionLoggerService userActionLogger)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _vwUserSignInResponseRepository = vwUserSignInResponseRepository;
            _configuration = configuration;
            _userActionLogger = userActionLogger;
        }

        public async Task<ServiceResult<LoginResponse>> Exeute(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = (User?)(IsValidEmail(loginDto.EmailOrUsername ?? "") ? await _userRepository.SingleOrDefaultAsync(x => x.Email == loginDto.EmailOrUsername, cancellationToken: cancellationToken).ConfigureAwait(false) :
                 await _userRepository.SingleOrDefaultAsync(x => x.Username == (loginDto.EmailOrUsername ?? "").ToLower(), cancellationToken: cancellationToken).ConfigureAwait(false));
            if (string.IsNullOrEmpty(loginDto.EmailOrUsername))
            {
                throw new ArgumentException("Wrong username");
            }
            if (string.IsNullOrEmpty(loginDto.Password))
            {
                throw new ArgumentException("Wrong password");
            }
            if (user == null)
            {
                var errorMessage = $"User {loginDto.EmailOrUsername} not found";
                await _userActionLogger.Execute(userId: null, actionName: "Auth", actionResult: "Failed", ErrorReason: errorMessage, cancellationToken: cancellationToken).ConfigureAwait(false);
                throw new ArgumentException("User not found.");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    var errorMessage = $"User {loginDto.EmailOrUsername}: Incorrect password";
                    await _userActionLogger.Execute(userId: null, actionName: "Auth", actionResult: "Failed", ErrorReason: errorMessage, cancellationToken: cancellationToken).ConfigureAwait(false);
                    throw new ArgumentException(errorMessage);
                }
            }
            var jwtSecret = _configuration["Jwt:Secret"] ?? throw new NotFoundException("Jwt Secret Key not found");

            var generateToken = GenerateJwtToken(user, jwtSecret, false);
            var generateRefreshToken = GenerateJwtToken(user, jwtSecret, true);

            var checkRefreshTokenIfExists = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken).ConfigureAwait(false);
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
                await _refreshTokenRepository.Add(refreshTokenObject, cancellationToken).ConfigureAwait(false);
            }


            var getUser = await _vwUserSignInResponseRepository.Where(x => x.Id == user.Id).Select(op => new LoginResponse()
            {
                Id = op.Id,
                Email = op.Email,
                FirstName = op.FirstName ?? "",
                LastName = op.LastName ?? "",
                AccessToken = generateToken.Item1,
                LoginResponsePositions = new LoginResponsePositions()
                {
                    PositionId = op.JobPositionId,
                    Position = op.JobPositionName,
                    DepartmentId = op.DepartmentId,
                    Department = op.DepartmentName
                }
            }).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (getUser == null)
            {
                var errorMessage = $"User in id {user.Id} no found";
                await _userActionLogger.Execute(userId: user.Id, actionName: "Auth", actionResult: "Failed", ErrorReason: errorMessage, cancellationToken: cancellationToken).ConfigureAwait(false);
                throw new NotFoundException(errorMessage);
            }

            await _userActionLogger.Execute(userId: user.Id, actionName: "Auth", actionResult: "Success", ErrorReason: null, cancellationToken: cancellationToken).ConfigureAwait(false);
            return ServiceResult<LoginResponse>.SuccessResult(getUser);
        }

        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new(pattern);
            Match match = regex.Match(email);
            return match.Success;
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
