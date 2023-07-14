using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.UserRoles.AddOrUpdateUserRole
{
    public interface IAddOrUpdateUserRoleService
    {
        Task<ServiceResult<bool>> Execute(AddOrUpdateUserRoleRequest request, CancellationToken cancellationToken);
    }
}
