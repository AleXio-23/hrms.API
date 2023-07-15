using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.RoleClaims.GetRoleClaims
{
    public interface IGetRoleClaimsService
    {
        Task<ServiceResult<List<ClaimsDTO>>> Execute(int roleId, CancellationToken cancellationToken);
    }
}
