using hrms.Domain.Models.User.AddNewUser;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.AddNewUser
{
    public interface IAddNewUserService
    {
        Task<ServiceResult<int>> Execute(AddNewUserRequest request, CancellationToken cancellationToken);
    }
}
