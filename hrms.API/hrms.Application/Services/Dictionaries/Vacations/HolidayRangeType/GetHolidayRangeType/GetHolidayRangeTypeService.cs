using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeType
{
    public class GetHolidayRangeTypeService : IGetHolidayRangeTypeService
    {
        private readonly IRepository<Persistance.Entities.HolidayRangeType> _holidayRangeTypeRepository;
        private readonly IMapper _mapper;

        public GetHolidayRangeTypeService(IRepository<Persistance.Entities.HolidayRangeType> holidayRangeTypeRepository, IMapper mapper)
        {
            _holidayRangeTypeRepository = holidayRangeTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<HolidayRangeTypeDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) throw new ArgumentException("Wrong id value");

            var getType = await _holidayRangeTypeRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Holiday range type on id {id} not found");
            var resultDto = _mapper.Map<HolidayRangeTypeDTO>(getType);

            return ServiceResult<HolidayRangeTypeDTO>.SuccessResult(resultDto);
        }
    }
}
