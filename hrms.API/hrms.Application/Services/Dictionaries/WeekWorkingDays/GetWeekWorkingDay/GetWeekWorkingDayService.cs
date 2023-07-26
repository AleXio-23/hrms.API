using AutoMapper;
using hrms.Domain.Models.Dictionary.WeekWorkingDays;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDay
{
    public class GetWeekWorkingDayService : IGetWeekWorkingDayService
    {
        private readonly IRepository<WeekWorkingDay> _weekWorkingDayRepository;
        private readonly IMapper _mapper;

        public GetWeekWorkingDayService(IRepository<WeekWorkingDay> weekWorkingDayRepository, IMapper mapper)
        {
            _weekWorkingDayRepository = weekWorkingDayRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WeekWorkingDayDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            var getReslt = await _weekWorkingDayRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Week working day on id {id} notfound");
            var mappedResultDto = _mapper.Map<WeekWorkingDayDTO>(getReslt);

            return ServiceResult<WeekWorkingDayDTO>.SuccessResult(mappedResultDto);
        }

        public async Task<ServiceResult<WeekWorkingDayDTO>> Execute(string weekdayCode, CancellationToken cancellationToken)
        {
            var getReslt = await _weekWorkingDayRepository.FirstOrDefaultAsync(x => x.Code.ToLower().Contains(weekdayCode.ToLower()), cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Week working day {weekdayCode} notfound");
            var mappedResultDto = _mapper.Map<WeekWorkingDayDTO>(getReslt);

            return ServiceResult<WeekWorkingDayDTO>.SuccessResult(mappedResultDto);
        }
    }
}
