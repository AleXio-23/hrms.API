using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayTypes
{
    public class GetHolidayTypesService : IGetHolidayTypesService
    {
        private readonly IRepository<Persistance.Entities.HolidayType> _holidayTypeRepository;
        private readonly IMapper _mapper;

        public GetHolidayTypesService(IRepository<Persistance.Entities.HolidayType> holidayTypeRepository, IMapper mapper)
        {
            _holidayTypeRepository = holidayTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<HolidayTypeDTO>>> Execute(CancellationToken cancellationToken)
        {
            var getRangeTypes = await _holidayTypeRepository.GetAllAsQueryable()
               .Select(x => _mapper.Map<HolidayTypeDTO>(x))
               .ToListAsync(cancellationToken).ConfigureAwait(false) ?? new List<HolidayTypeDTO>();

            return ServiceResult<List<HolidayTypeDTO>>.SuccessResult(getRangeTypes);
        }
    }
}
