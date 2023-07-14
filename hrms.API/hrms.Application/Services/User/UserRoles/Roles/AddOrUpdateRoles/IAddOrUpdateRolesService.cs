using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.Roles.AddOrUpdateRoles
{
    public interface IAddOrUpdateRolesService
    {
        Task<ServiceResult<RoleDTO>> Execute(RoleDTO roleDTO, CancellationToken cancellationToken);
    }
}
