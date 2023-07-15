using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.RoleClaims.GetRoleClaim
{
    public interface IGetRoleClaimService
    {
        Task<ServiceResult<ClaimsDTO>> Execute(int roleId, int claimId, CancellationToken cancellationToken);
    }
}
