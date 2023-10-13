using hrms.Domain.Models.User.AddNewUser;
using hrms.Domain.Models.User.Employees;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.GetEmployees
{
    public interface IGetEmployeesService
    { 
        Task<ServiceResult<List<EmployeesListResponse>>> Execute(EmployeesFilter filter, CancellationToken cancellationToken);
    }
}
