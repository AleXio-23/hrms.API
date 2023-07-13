using hrms.Domain.Models.Dictionary.Departments;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.GetDepartment
{
    public interface IGetDepartmentService
    {
        Task<ServiceResult<DepartmentDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
