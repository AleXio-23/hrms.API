using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Departments.DeleteDepartment
{
    public interface IDeleteDepartmentService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
    }
}
