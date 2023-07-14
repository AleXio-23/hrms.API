using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.Roles.GetRole
{
    public interface IGetRoleService
    {
        Task<ServiceResult<RoleDTO>> Execute(int roleId, CancellationToken cancellationToken);
    }
}
