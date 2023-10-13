using hrms.Shared.Models;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface IResetPasswordService
    {
        Task<ServiceResult<string>> Execute(string usernameOrEmail, CancellationToken cancellationToken);
    }
}
