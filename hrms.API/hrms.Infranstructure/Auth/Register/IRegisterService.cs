using hrms.Domain.Models.Auth;
using hrms.Shared.Models;

namespace hrms.Infranstructure.Auth.Register
{
    public interface IRegisterService
    {
        Task<ServiceResult<string>> Execute(RegisterDto registerDto, CancellationToken cancellationToken);
    }
}
