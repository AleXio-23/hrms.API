using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeTypes
{
    public class GetHolidayRangeTypesService : IGetHolidayRangeTypesService
    {
        private readonly IRepository<Persistance.Entities.HolidayRangeType> _holidayRangeTypeRepository;
        private readonly IMapper _mapper;

        public GetHolidayRangeTypesService(IRepository<Persistance.Entities.HolidayRangeType> holidayRangeTypeRepository, IMapper mapper)
        {
            _holidayRangeTypeRepository = holidayRangeTypeRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<List<HolidayRangeTypeDTO>>> Execute(CancellationToken cancellationToken)
        {
            var getRangeTypes = await _holidayRangeTypeRepository.GetAllAsQueryable()
                .Select(x => _mapper.Map<HolidayRangeTypeDTO>(x))
                .ToListAsync(cancellationToken).ConfigureAwait(false) ?? new List<HolidayRangeTypeDTO>();

            return ServiceResult<List<HolidayRangeTypeDTO>>.SuccessResult(getRangeTypes);
        }
    }
}
