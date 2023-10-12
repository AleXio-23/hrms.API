using hrms.Domain.Models.User.AddNewUser;
using hrms.Domain.Models.User.Employees;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.GetEmployees
{
    public class GetEmployeesService : IGetEmployeesService
    {
        private readonly IRepository<Persistance.Entities.User> _userRepository;

        public Task<ServiceResult<List<AddNewUserRequest>>> Execute(EmployeesFilter filter, CancellationToken cancellationToken)
        {
            var getUser = _userRepository.GetIncluding(x => x.UserProfile, 
                x => x.UserLocation,
                x => x.UserJobPositions.AsQueryable().Include(uj => uj.Position).Include(uj => uj.Department),
                );

            throw new NotImplementedException();
        }
    }
}
