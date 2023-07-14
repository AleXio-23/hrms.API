using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.Roles.GetRole
{
    public class GetRoleService : IGetRoleService
    {
        private readonly IRepository<Role> _rolesRepository;
        private readonly IMapper _mapper;

        public GetRoleService(IRepository<Role> rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<RoleDTO>> Execute(int roleId, CancellationToken cancellationToken)
        {
            if (roleId < 1) { throw new ArgumentException("Wrong id format"); }

            var getExistingRole = await _rolesRepository
                                          .Get(roleId, cancellationToken)
                                          .ConfigureAwait(false) ?? throw new NotFoundException($"Role on Id: {roleId} not found");
            var getMappedDto = _mapper.Map<RoleDTO>(getExistingRole);
            return new ServiceResult<RoleDTO>
            {
                Success = true,
                ErrorOccured = false,
                Data = getMappedDto
            };
        }
    }
}
