using hrms.Domain.Models.User.Employees;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.GetEmployees
{
    public class GetEmployeesService : IGetEmployeesService
    {
        private readonly IRepository<Persistance.Entities.User> _userRepository;

        public GetEmployeesService(IRepository<Persistance.Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<List<EmployeesListResponse>>> Execute(EmployeesFilter filter, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            var query = _userRepository.GetIncluding(
                 x => x.UserRoles,
                 x => x.UserProfile,
                 x => x.UserLocation,
                 x => x.UserJobPositions,
                 x => x.UsersWorkSchedules
             );
#pragma warning restore CS8603 // Possible null reference return.

            query = query.Include(x => x.UserJobPositions).ThenInclude(uj => uj.Position);
            query = query.Include(x => x.UserJobPositions).ThenInclude(uj => uj.Department);
            query = query.Include(x => x.UserRoles).ThenInclude(uj => uj.Role);


            if (!string.IsNullOrWhiteSpace(filter.Fullname))
            {
                var fullName = filter.Fullname.Trim();
                query = query.Where(x =>
                             x.UserProfile != null &&
                             !string.IsNullOrWhiteSpace(x.UserProfile.Firstname) &&
                             !string.IsNullOrWhiteSpace(x.UserProfile.Lastname) &&
                             (x.UserProfile.Firstname + " " + x.UserProfile.Lastname).Contains(fullName));
            }

            if (filter.Departments != null && filter.Departments.Count > 0)
            {
                query = query.Where(x =>
                        x.UserJobPositions != null &&
                        x.UserJobPositions.Any(jp =>
                                jp.DepartmentId != null &&
                                filter.Departments.Contains(jp.DepartmentId.Value)
                            )
                        );
            }
            if (!string.IsNullOrEmpty(filter.Phone))
            {
                query = query.Where(x => x.UserProfile != null && ((x.UserProfile.PhoneNumber1 != null && x.UserProfile.PhoneNumber1.Contains(filter.Phone)) || (x.UserProfile.PhoneNumber2 != null && x.UserProfile.PhoneNumber2.Contains(filter.Phone))));
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(x => x.Email != null && x.Email.Contains(filter.Email));
            }

            if (filter.Countries != null && filter.Countries.Count > 0)
            {
                query = query.Where(x => x.UserLocation != null && x.UserLocation.CountryId != null && filter.Countries.Contains(x.UserLocation.CountryId.Value));
            }

            if (filter.States != null && filter.States.Count > 0)
            {
                query = query.Where(x => x.UserLocation != null && x.UserLocation.StateId != null && filter.States.Contains(x.UserLocation.StateId.Value));
            }

            if (filter.Cities != null && filter.Cities.Count > 0)
            {
                query = query.Where(x => x.UserLocation != null && x.UserLocation.CityId != null && filter.Cities.Contains(x.UserLocation.CityId.Value));
            }

            if (filter.Roles != null && filter.Roles.Count > 0)
            {
                query = query.Where(x =>
                        x.UserRoles != null &&
                        x.UserRoles.Any(jp =>
                                jp.Role != null &&
                                filter.Roles.Contains(jp.Role.Id)
                            )
                        );
            }


            var getResult = await query.Select(x => new EmployeesListResponse()
            {
                UserId = x.Id,
                Firstname = x.UserProfile != null ? x.UserProfile.Firstname : null,
                Lastname = x.UserProfile != null ? x.UserProfile.Lastname : null,
                Department = x.UserJobPositions != null
                                && x.UserJobPositions.FirstOrDefault() != null
                                && x.UserJobPositions!.FirstOrDefault()!.Department != null
                                    ? x.UserJobPositions.FirstOrDefault()!.Department!.Name
                                    : null,
                Phone = x.UserProfile != null ? GetPhonenumber(x.UserProfile.PhoneNumber1, x.UserProfile!.PhoneNumber2) : string.Empty,
                Email = x.Email,
                Country = x.UserLocation != null && x.UserLocation.Country != null ? x.UserLocation!.Country!.Name : null,
                City = x.UserLocation != null && x.UserLocation.City != null ? x.UserLocation!.City!.Name : null,
                Role = x.UserRoles != null ?
                    string.Join(", ", x.UserRoles.Where(x => x.Role != null && !string.IsNullOrEmpty(x.Role.Name)).Select(x => x.Role!.Name))
                        : string.Empty


            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<EmployeesListResponse>>.SuccessResult(getResult);
        }

        private static string GetPhonenumber(string? phone1, string? phone2)
        {
            if (!string.IsNullOrEmpty(phone1))
            {
                return phone1;
            }

            return phone2 ?? string.Empty;
        }
    }
}
