using hrms.Shared.Models;

namespace hrms.Infranstructure.Auth.AccessToken.UpdateAccessToken
{
    public interface IUpdateAccessTokenService
    {
        Task<ServiceResult<string>> Execute(string? accessToken, CancellationToken cancellationToken);
    }
}
