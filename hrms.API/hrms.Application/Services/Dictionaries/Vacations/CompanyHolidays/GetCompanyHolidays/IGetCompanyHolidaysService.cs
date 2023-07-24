using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays
{
    public interface IGetCompanyHolidaysService
    {
        Task<ServiceResult<List<CompanyHolidayDTO>>> Execute(CompanyHolidayFilter filter, CancellationToken cancellationToken);
    }
}
