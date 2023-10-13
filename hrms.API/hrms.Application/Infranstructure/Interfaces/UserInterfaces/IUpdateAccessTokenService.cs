using hrms.Shared.Models;

namespace hrms.Application.Infranstructure.Interfaces.UserInterfaces
{
    public interface IUpdateAccessTokenService
    {
        Task<ServiceResult<string>> Execute(string? accessToken, CancellationToken cancellationToken);
    }
}
