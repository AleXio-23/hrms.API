using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.RoleClaims.GetRoleClaims
{
    public class GetRoleClaimsService: IGetRoleClaimsService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleClaimsService(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<ClaimsDTO>>> Execute(int roleId, CancellationToken cancellationToken)
        {
            if (roleId < 1)
            {
                throw new ArgumentException("Wrong Id number");
            }

            var getRoleWithClaims = await _roleRepository.GetIncluding(x => x.Claims)
                .FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Role on Id: {roleId} not found");

            var getClaims = _mapper.Map<List<ClaimsDTO>>(getRoleWithClaims.Claims.ToList());

            return ServiceResult<List<ClaimsDTO>>.SuccessResult(getClaims);
        }
    }
}
