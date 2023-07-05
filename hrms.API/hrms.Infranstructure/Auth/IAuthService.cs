using hrms.Domain.Models.Auth;
using hrms.Persistance.Entities;

namespace hrms.Infranstructure.Auth
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDto registerDto, CancellationToken cancellationToken);
        Task<string> Login(LoginDto loginDto, CancellationToken cancellationToken);
        string GenerateJwtToken(User user);
    }
}
