using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.RoleClaims.DeleteRoleClaims
{
    public interface IDeleteRoleClaimsService
    {
        Task<ServiceResult<bool>> Execute(RoleClaimsDTO roleClaimsDTO, CancellationToken cancellationToken);
    }
}
