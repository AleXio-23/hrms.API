using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.Roles.GetRoles
{
    public interface IGetRolesService
    {
        Task<ServiceResult<List<RoleDTO>>> Execute(RolesFilter filter, CancellationToken cancellationToken);
    }
}
