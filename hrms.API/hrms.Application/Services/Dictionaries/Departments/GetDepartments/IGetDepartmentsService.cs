using hrms.Domain.Models.Dictionary.Departments;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.GetDepartments
{
    public interface IGetDepartmentsService
    {
        Task<ServiceResult<List<DepartmentDTO>>> Execute(DepartmentFilter filter, CancellationToken cancellationToken);
    }
}
