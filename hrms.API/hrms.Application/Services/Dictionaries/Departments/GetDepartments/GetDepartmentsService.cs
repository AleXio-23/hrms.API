using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Departments.GetDepartments
{
    public class GetDepartmentsService : IGetDepartmentsService
    {
        private readonly IRepository<Department> _departmentRepository;

        public GetDepartmentsService(IRepository<Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ServiceResult<List<DepartmentDTO>>> Execute(DepartmentFilter filter, CancellationToken cancellationToken)
        {
            var query = _departmentRepository.GetAllAsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }

            var result = await query.Select(x => new DepartmentDTO()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            }).ToListAsync(cancellationToken);

            return ServiceResult<List<DepartmentDTO>>.SuccessResult(result ?? new List<DepartmentDTO>());
        }
    }
}
