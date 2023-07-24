using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHoliday
{
    public interface IGetCompanyHolidayService
    {
        Task<ServiceResult<CompanyHolidayDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
