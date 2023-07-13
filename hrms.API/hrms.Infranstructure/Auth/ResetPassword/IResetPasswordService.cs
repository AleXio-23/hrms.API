using hrms.Shared.Models;

namespace hrms.Infranstructure.Auth.ResetPassword
{
    public interface IResetPasswordService
    {
        Task<ServiceResult<string>> Execute(string usernameOrEmail, CancellationToken cancellationToken);
    }
}
