using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.DeleteCompanyHoliday
{
    public class DeleteCompanyHolidayService : IDeleteCompanyHolidayService
    {
        private readonly IRepository<CompanyHoliday> _companyHolidayRepository;

        public DeleteCompanyHolidayService(IRepository<CompanyHoliday> companyHolidayRepository)
        {
            _companyHolidayRepository = companyHolidayRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 0)
            {
                throw new ArgumentException("Wrong id value");
            }

            await _companyHolidayRepository.Delete(id, cancellationToken).ConfigureAwait(false);
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> Execute(List<int> id, CancellationToken cancellationToken)
        {
            var getCompanyHolidays = await _companyHolidayRepository.Where(x => id.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            await _companyHolidayRepository.DeleteRange(getCompanyHolidays, cancellationToken).ConfigureAwait(false);
            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
