using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHoliday
{
    public class GetCompanyHolidayService : IGetCompanyHolidayService
    {
        private readonly IRepository<CompanyHoliday> _companyHolidayRepository;
        private readonly IMapper _mapper;

        public GetCompanyHolidayService(IRepository<CompanyHoliday> companyHolidayRepository, IMapper mapper)
        {
            _companyHolidayRepository = companyHolidayRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CompanyHolidayDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Wrong id values");
            }

            var getHolday = await _companyHolidayRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Holiday on id {id} not found");
            var mappedResult = _mapper.Map<CompanyHolidayDTO>(getHolday);
            return ServiceResult<CompanyHolidayDTO>.SuccessResult(mappedResult);
        }
    }
}
