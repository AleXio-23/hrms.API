using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
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
            var getHolday = await _companyHolidayRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Holiday on id {id} not found");
            getHolday.IsActive = false;

            await _companyHolidayRepository.Update(getHolday, cancellationToken).ConfigureAwait(false);
            return ServiceResult<bool>.SuccessResult(true);
        }

        public async Task<ServiceResult<bool>> Execute(List<int> ids, CancellationToken cancellationToken)
        {
            foreach (var id in ids)
            {
                await _companyHolidayRepository.Delete(id, cancellationToken).ConfigureAwait(false);
            }
            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
