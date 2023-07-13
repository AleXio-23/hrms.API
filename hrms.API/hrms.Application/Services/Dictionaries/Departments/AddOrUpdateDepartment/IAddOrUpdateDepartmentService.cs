using hrms.Domain.Models.Dictionary.Departments;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.AddOrUpdateDepartment
{
    public interface IAddOrUpdateDepartmentService
    {
        Task<ServiceResult<DepartmentDTO>> Execute(DepartmentDTO departmentDTO, CancellationToken cancellationToken);
    }
}
