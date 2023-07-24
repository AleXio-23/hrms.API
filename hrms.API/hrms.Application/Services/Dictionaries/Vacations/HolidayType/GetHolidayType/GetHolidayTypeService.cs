using AutoMapper;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayType
{
    public class GetHolidayTypeService : IGetHolidayTypeService
    {
        private readonly IRepository<Persistance.Entities.HolidayType> _holidayTypeRepository;
        private readonly IMapper _mapper;

        public GetHolidayTypeService(IRepository<Persistance.Entities.HolidayType> holidayTypeRepository, IMapper mapper)
        {
            _holidayTypeRepository = holidayTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<HolidayTypeDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id <= 0) throw new ArgumentException("Wrong id value");

            var getType = await _holidayTypeRepository
                .Get(id, cancellationToken)
                .ConfigureAwait(false) ?? throw new NotFoundException($"Holiday type on id {id} not found");
            var resultDto = _mapper.Map<HolidayTypeDTO>(getType);

            return ServiceResult<HolidayTypeDTO>.SuccessResult(resultDto);
        }
    }
}
