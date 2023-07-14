using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UserRoles.Roles.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IRepository<Role> _rolesRepository;
        private readonly IMapper _mapper;

        public GetRolesService(IRepository<Role> rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<RoleDTO>>> Execute(RolesFilter filter, CancellationToken cancellationToken)
        {
            var roles = _rolesRepository.GetAllAsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                roles = roles.Where(x => x.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter.Description))
            {
                roles = roles.Where(x => x.Description != null && x.Description.Contains(filter.Description));
            }

            var getRoleDtoList = await roles
                                    .Select(x => _mapper.Map<RoleDTO>(x))
                                    .ToListAsync(cancellationToken);

            return new ServiceResult<List<RoleDTO>>()
            {
                Success = true,
                ErrorOccured = false,
                Data = getRoleDtoList
            };
        }
    }
}
