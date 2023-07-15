using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.RoleClaims.GetRoleClaim
{
    public class GetRoleClaimService : IGetRoleClaimService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleClaimService(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ClaimsDTO>> Execute(int roleId, int claimId, CancellationToken cancellationToken)
        {
            if (roleId < 1)
            {
                throw new ArgumentException("Wrong Id number");
            }

            var getRoleWithClaims = await _roleRepository.GetIncluding(x => x.Claims)
                .FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Role on Id: {roleId} not found");

            var getClaims = _mapper.Map<ClaimsDTO>(getRoleWithClaims.Claims.FirstOrDefault(x => x.Id == claimId));

            return new ServiceResult<ClaimsDTO>()
            {
                Success = true,
                ErrorOccured = false,
                Data = getClaims
            };
        }
    }
}
