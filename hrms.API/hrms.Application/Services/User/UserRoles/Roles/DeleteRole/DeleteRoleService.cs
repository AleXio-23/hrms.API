using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UserRoles.Roles.DeleteRole
{
    public class DeleteRoleService : IDeleteRoleService
    {
        private readonly IRepository<Role> _rolesRepository;

        public DeleteRoleService(IRepository<Role> rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int roleId, CancellationToken cancellationToken)
        {
            if (roleId < 1) { throw new ArgumentException("Wrong id format"); }

            var getExistingRole = await _rolesRepository
                                            .GetIncluding(x => x.Users)
                                            .FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken)
                                            .ConfigureAwait(false) ?? throw new NotFoundException($"Role on Id: {roleId} not found");
            if (getExistingRole.Users.Count > 0)
            {
                throw new ArgumentException($"Can't remove role on Id: {roleId}. It's assigned on some users");
            }

            getExistingRole.IsActive = false;
            var updateRole = await _rolesRepository.Update(getExistingRole, cancellationToken).ConfigureAwait(false);
            return new ServiceResult<bool>()
            {
                Success = true,
                Data = true,
                ErrorOccured = false,
            };
        }
    }
}
