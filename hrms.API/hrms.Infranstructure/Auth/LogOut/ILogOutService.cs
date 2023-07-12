using hrms.Shared.Models;

namespace hrms.Infranstructure.Auth.LogOut
{
    public interface ILogOutService
    {
        Task<ServiceResult<bool>> Execute(string? accessToken, CancellationToken cancellationToken = default);
    }
}
