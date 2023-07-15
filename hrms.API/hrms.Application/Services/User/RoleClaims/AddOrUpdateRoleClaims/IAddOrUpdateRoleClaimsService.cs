using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.RoleClaims.AddOrUpdateRoleClaims
{
    public interface IAddOrUpdateRoleClaimsService
    {
        Task<ServiceResult<RoleClaimsDTO>> Execute(RoleClaimsDTO roleClaimsDTO, CancellationToken cancellationToken);
    }
}
