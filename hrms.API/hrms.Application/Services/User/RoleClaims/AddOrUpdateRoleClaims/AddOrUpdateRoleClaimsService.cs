using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.RoleClaims.AddOrUpdateRoleClaims
{
    public class AddOrUpdateRoleClaimsService : IAddOrUpdateRoleClaimsService
    {
        private readonly IRepository<Claim> _claimRepository;
        private readonly IRepository<Role> _roleRepository;

        public AddOrUpdateRoleClaimsService(IRepository<Claim> claimRepository, IRepository<Role> roleRepository)
        {
            _claimRepository = claimRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ServiceResult<RoleClaimsDTO>> Execute(RoleClaimsDTO roleClaimsDTO, CancellationToken cancellationToken)
        {
            if (roleClaimsDTO.RoleId < 1) throw new ArgumentException("Wrong role id");
            if (roleClaimsDTO.ClaimId < 1) throw new ArgumentException("Wrong claim id");

            var getRole = await _roleRepository.GetIncluding(x => x.Claims).FirstOrDefaultAsync(x => x.Id == roleClaimsDTO.RoleId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Role on id: {roleClaimsDTO.RoleId} not found");
            var getClaim = await _claimRepository.Get(roleClaimsDTO.ClaimId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Claim on Id: {roleClaimsDTO.ClaimId} not found");

            if (getRole.Claims.Any(x => x.Id == roleClaimsDTO.RoleId))
            {
                throw new ArgumentException($"Claim with Id: {roleClaimsDTO.ClaimId} on role with Id: {roleClaimsDTO.RoleId} exists");
            }

            getRole.Claims.Add(getClaim);
            await _roleRepository.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<RoleClaimsDTO>.SuccessResult(roleClaimsDTO);
        }
    }
}
