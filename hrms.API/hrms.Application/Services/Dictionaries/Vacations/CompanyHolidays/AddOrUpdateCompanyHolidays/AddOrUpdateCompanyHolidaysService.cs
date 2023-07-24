using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.AddOrUpdateCompanyHolidays
{
    public class AddOrUpdateCompanyHolidaysService : IAddOrUpdateCompanyHolidaysService
    {
        private readonly IRepository<CompanyHoliday> _companyHolidayRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateCompanyHolidaysService(IRepository<CompanyHoliday> companyHolidayRepository, IMapper mapper)
        {
            _companyHolidayRepository = companyHolidayRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CompanyHolidayDTO>> Execute(CompanyHolidayDTO companyHolidayDTO, CancellationToken cancellationToken)
        {
            if (companyHolidayDTO.Id == null || companyHolidayDTO.Id <= 0)
            {
                var createNewHoliday = new CompanyHoliday()
                {
                    EventDate = companyHolidayDTO.EventDate ?? throw new ArgumentException("You must provide correct event date"),
                    EventDescription = companyHolidayDTO.EventDescription,
                    NotifyBeforeDays = companyHolidayDTO.NotifyBeforeDays,
                    NotifyBeforeHours = companyHolidayDTO.NotifyBeforeHours,
                    IsActive = true
                };

                var result = await _companyHolidayRepository.Add(createNewHoliday, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<CompanyHolidayDTO>(result);

                return ServiceResult<CompanyHolidayDTO>.SuccessResult(resultDto);
            }
            else
            {
                var getExistingHiliday = await _companyHolidayRepository
                    .Get(companyHolidayDTO.Id ?? throw new ArgumentException("Something went wrong getting existing holiday event"), cancellationToken)
                    .ConfigureAwait(false) ?? throw new NotFoundException($"Holiday on id {companyHolidayDTO.Id} not found");

                getExistingHiliday.EventDate = companyHolidayDTO.EventDate ?? throw new ArgumentException("You must provide correct event date");
                getExistingHiliday.EventDescription = companyHolidayDTO.EventDescription;
                getExistingHiliday.NotifyBeforeDays = companyHolidayDTO.NotifyBeforeDays;
                getExistingHiliday.NotifyBeforeHours = companyHolidayDTO.NotifyBeforeHours;
                getExistingHiliday.IsActive = true;

                return ServiceResult<CompanyHolidayDTO>.SuccessResult(companyHolidayDTO);
            }
        }
    }
}
