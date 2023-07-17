using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace hrms.Infranstructure.Auth.LogOut
{
    public class LogOutService : ILogOutService
    {

        private readonly IRepository<RefreshToken> _refreshTokenRepository;

        public LogOutService(IRepository<RefreshToken> refreshTokenRepository)
        {
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
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId not found in access token");
            }

            var refreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(x => x.UserId.ToString() == userId, cancellationToken).ConfigureAwait(false) ?? throw new ArgumentException("Refresh token not found");
            await _refreshTokenRepository.Delete(refreshToken, cancellationToken).ConfigureAwait(false);
             
            return ServiceResult<bool>.SuccessResult(true); 
        }
    }
}
