using hrms.Domain.Models.Auth;
using hrms.Persistance.Entities;
using hrms.Shared.Models;

namespace hrms.Infranstructure.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<string>> Register(RegisterDto registerDto, CancellationToken cancellationToken);
        Task<ServiceResult<LoginResponse>> Login(LoginDto loginDto, CancellationToken cancellationToken);
        Task<ServiceResult<string>> UpdateAccessToken(string? accessToken, CancellationToken cancellationToken);
        Task<ServiceResult<string>> ResetPassword(string usernameOrEmail, CancellationToken cancellationToken);
    }
}
