using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using System.IdentityModel.Tokens.Jwt;

namespace hrms.Infranstructure.Auth.LogOut
{
    public class LogOutService : ILogOutService
    {
        private readonly IUserActionLoggerService _userActionLogger;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public LogOutService(IUserActionLoggerService userActionLogger, IRepository<RefreshToken> refreshTokenRepository)
        {
            _userActionLogger = userActionLogger;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ServiceResult<bool>> Execute(string? accessToken, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Wrong accessToken");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);
            string? userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var userTryInt = Int32.TryParse(userId, out var userInt);
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId not found in access token");
            }

            var refreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.UserId.ToString() == userId, cancellationToken).ConfigureAwait(false) ?? throw new ArgumentException("Refresh token not found");
            await _refreshTokenRepository.Delete(refreshToken, cancellationToken).ConfigureAwait(false);

            await _userActionLogger.Execute(userId: userInt, actionName: "LogOut", actionResult: "Success", ErrorReason: null, cancellationToken: cancellationToken);
            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
