using AutoMapper;
using hrms.Domain.Models.Dictionary.WeekWorkingDays;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDays
{
    public class GetWeekWorkingDaysService : IGetWeekWorkingDaysService
    {
        private readonly IRepository<WeekWorkingDay> _weekWorkingDayRepository;
        private readonly IMapper _mapper;

        public GetWeekWorkingDaysService(IRepository<WeekWorkingDay> weekWorkingDayRepository, IMapper mapper)
        {
            _weekWorkingDayRepository = weekWorkingDayRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<WeekWorkingDayDTO>>> Execute(CancellationToken cancellationToken)
        {
            var getDays = await _weekWorkingDayRepository.GetAllAsQueryable()
                .Select(x => _mapper.Map<WeekWorkingDayDTO>(x))
                .ToListAsync(cancellationToken).ConfigureAwait(false) ?? new List<WeekWorkingDayDTO>();

            return ServiceResult<List<WeekWorkingDayDTO>>.SuccessResult(getDays);
        }
    }
}
