using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.RoleClaims.DeleteRoleClaims
{
    public class DeleteRoleClaimsService : IDeleteRoleClaimsService
    {
        private readonly IRepository<Role> _roleRepository;

        public DeleteRoleClaimsService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ServiceResult<bool>> Execute(RoleClaimsDTO roleClaimsDTO, CancellationToken cancellationToken)
        {
            var getRole = await _roleRepository.GetIncluding(x => x.Claims).FirstOrDefaultAsync(x => x.Id == roleClaimsDTO.RoleId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Role on Id: {roleClaimsDTO.RoleId} not found");
            var getClaim = getRole?.Claims.FirstOrDefault(x => x.Id == roleClaimsDTO.ClaimId) ?? throw new NotFoundException($"Claim with id: {roleClaimsDTO.ClaimId} on role with Id: {roleClaimsDTO.RoleId} not found");

            getRole.Claims.Remove(getClaim);

            await _roleRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
