using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.GetUser
{
    public interface IGetUserService
    {
        Task<ServiceResult<UserDTO>> Execute(int userId, CancellationToken cancellationToken);
    }
}
