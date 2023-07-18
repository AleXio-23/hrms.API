using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.Roles.AddOrUpdateRoles
{
    public class AddOrUpdateRolesService : IAddOrUpdateRolesService
    {
        private readonly IRepository<Role> _rolesRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateRolesService(IRepository<Role> rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<RoleDTO>> Execute(RoleDTO roleDTO, CancellationToken cancellationToken)
        {
            //Add new role
            if (roleDTO.Id == null && roleDTO.Id < 1)
            {
                var newRole = _mapper.Map<Role>(roleDTO);
                var createNewRole = await _rolesRepository.Add(newRole, cancellationToken).ConfigureAwait(false);
                roleDTO.Id = newRole.Id;

                return ServiceResult<RoleDTO>.SuccessResult(roleDTO);
            }
            //Update role
            else if (roleDTO.Id > 0)
            {
                var getExistingRole = await _rolesRepository
                                            .Get(roleDTO.Id ?? throw new ArgumentException("Unexpected role id value"), cancellationToken)
                                            .ConfigureAwait(false) ?? throw new NotFoundException($"Role in Id: {roleDTO.Id} not found");
                getExistingRole.IsActive = roleDTO.IsActive;
                getExistingRole.Name = roleDTO.Name;
                getExistingRole.Description = roleDTO.Description;

                var updateRole = await _rolesRepository.Update(getExistingRole, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<RoleDTO>(updateRole);

                return ServiceResult<RoleDTO>.SuccessResult(resultDto);
            }
            else
            {
                throw new Exception("Unexpected error occured");
            }
        }
    }
}
