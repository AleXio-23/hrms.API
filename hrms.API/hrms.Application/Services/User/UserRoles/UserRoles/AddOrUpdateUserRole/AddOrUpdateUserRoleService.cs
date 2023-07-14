using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UserRoles.UserRoles.AddOrUpdateUserRole
{
    public class AddOrUpdateUserRoleService : IAddOrUpdateUserRoleService
    {
        private readonly IRepository<Persistance.Entities.User> _usersRepository;
        private readonly IRepository<Role> _rolesRepository;

        public AddOrUpdateUserRoleService(IRepository<Persistance.Entities.User> usersRepository, IRepository<Role> rolesRepository)
        {
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
        }

        public async Task<ServiceResult<bool>> Execute(AddOrUpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            if (request.RoleId < 1) throw new ArgumentException("Wrong id value");
            if (request.UserId < 1) throw new ArgumentException("Wrong id value");
            var user = await _usersRepository.GetIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken) ?? throw new NotFoundException($"User on Id: {request.UserId} not found");

            if (user.Roles != null && user.Roles.Count > 0)
            {
                if (user.Roles.Any(x => x.Id == request.RoleId))
                {
                    throw new ArgumentException("Role on this user exists");
                }
            }

            var role = await _rolesRepository.Get(request.RoleId, cancellationToken) ?? throw new NotFoundException($"Role on Id: {request.RoleId} not found");

            user?.Roles?.Add(role);
            await _usersRepository.SaveChangesAsync(cancellationToken);
            return new ServiceResult<bool>()
            {
                Success = true,
                Data = true,
                ErrorOccured = false 
            };
        }
    }
}
