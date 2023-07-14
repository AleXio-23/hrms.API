using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserRoles.UserRoles.AddOrUpdateUserRole
{
    public class AddOrUpdateUserRoleService : IAddOrUpdateUserRoleService
    {
        private readonly IRepository<Persistance.Entities.User> _usersRepository;
        private readonly IRepository<Role> _rolesRepository;
        private readonly IRepository<UserRole> _userRoleRepository;

        public AddOrUpdateUserRoleService(IRepository<Persistance.Entities.User> usersRepository, IRepository<Role> rolesRepository, IRepository<UserRole> userRoleRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<ServiceResult<bool>> Execute(AddOrUpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            if (request.RoleId < 1) throw new ArgumentException("Wrong id value");
            if (request.UserId < 1) throw new ArgumentException("Wrong id value");
            var userExists = await _usersRepository.AnyAsync(x => x.Id == request.UserId, cancellationToken).ConfigureAwait(false);


            var roleExists = await _rolesRepository.AnyAsync(x => x.Id == request.RoleId, cancellationToken).ConfigureAwait(false);

            var newUserRole = new UserRole()
            {
                UserId = request.UserId,
                RoleId = request.RoleId
            };
            await _userRoleRepository.Add(newUserRole, cancellationToken).ConfigureAwait(false);
            return new ServiceResult<bool>()
            {
                Success = true,
                Data = true,
                ErrorOccured = false
            };
        }
    }
}
