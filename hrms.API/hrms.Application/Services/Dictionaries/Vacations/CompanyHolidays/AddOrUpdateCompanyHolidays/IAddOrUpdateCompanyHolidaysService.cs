using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.AddOrUpdateCompanyHolidays
{
    public interface IAddOrUpdateCompanyHolidaysService
    {
        Task<ServiceResult<CompanyHolidayDTO>> Execute(CompanyHolidayDTO companyHolidayDTO, CancellationToken cancellationToken);
    }
}
