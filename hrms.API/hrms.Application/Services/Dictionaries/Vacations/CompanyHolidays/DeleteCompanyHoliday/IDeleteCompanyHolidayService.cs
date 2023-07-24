using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.DeleteCompanyHoliday
{
    public interface IDeleteCompanyHolidayService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
        Task<ServiceResult<bool>> Execute(List<int> id, CancellationToken cancellationToken);
    }
}
