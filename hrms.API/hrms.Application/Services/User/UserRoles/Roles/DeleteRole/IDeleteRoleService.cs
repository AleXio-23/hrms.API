using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.Roles.DeleteRole
{
    public interface IDeleteRoleService
    {
        Task<ServiceResult<bool>> Execute(int roleId, CancellationToken cancellationToken);
    }
}
