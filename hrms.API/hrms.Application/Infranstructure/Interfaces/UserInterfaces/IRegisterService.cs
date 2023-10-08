using hrms.Domain.Models.Auth;
using hrms.Shared.Models;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface IRegisterService
    {
        Task<ServiceResult<string>> Execute(RegisterDto registerDto, CancellationToken cancellationToken);
    }
}
