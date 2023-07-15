using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UpdateUser
{
    public interface IUpdateUserService
    {
        Task<ServiceResult< UserDTO>> Execute(UserDTO userDTO, CancellationToken cancellationToken);
    }
}
