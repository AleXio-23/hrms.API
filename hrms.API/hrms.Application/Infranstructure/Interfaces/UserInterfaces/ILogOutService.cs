using hrms.Shared.Models;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface ILogOutService
    {
        Task<ServiceResult<bool>> Execute(string? accessToken, CancellationToken cancellationToken = default);
    }
}
