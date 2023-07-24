using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays
{
    public class GetCompanyHolidaysService : IGetCompanyHolidaysService
    {
        private readonly IRepository<CompanyHoliday> _companyHolidayRepository;
        private readonly IMapper _mapper;

        public GetCompanyHolidaysService(IRepository<CompanyHoliday> companyHolidayRepository, IMapper mapper)
        {
            _companyHolidayRepository = companyHolidayRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<CompanyHolidayDTO>>> Execute(CompanyHolidayFilter filter, CancellationToken cancellationToken)
        {
            var query = _companyHolidayRepository.GetAllAsQueryable();

            if (filter.EventDateStart != null)
            {
                query = query.Where(x => x.EventDate >= filter.EventDateStart);
            }
            if (filter.EventDateEnd != null)
            {
                query = query.Where(x => x.EventDate <= filter.EventDateEnd);
            }
            if (!string.IsNullOrEmpty(filter.EventDescription))
            {
                query = query.Where(x => x.EventDescription != null && x.EventDescription.Contains(filter.EventDescription));
            }
            if (filter.NotifyBeforeDays != null)
            {
                query = query.Where(x => Equals(filter.NotifyBeforeDays));
            }
            if (filter.NotifyBeforeHours != null)
            {
                query = query.Where(x => x.Equals(filter.NotifyBeforeHours));
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.IsActive == filter.IsActive);
            }

            var result = await query
                .Select(x => _mapper.Map<CompanyHolidayDTO>(x))
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<CompanyHolidayDTO>>.SuccessResult(result);
        }
    }
}
