using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedule
{
    public class GetUsersWorkScheduleService : IGetUsersWorkScheduleService
    {
        private readonly IRepository<Persistance.Entities.UsersWorkSchedule> _usersRokScheduleRepository;
        private readonly IMapper _mapper;

        public GetUsersWorkScheduleService(IRepository<Persistance.Entities.UsersWorkSchedule> usersRokScheduleRepository, IMapper mapper)
        {
            _usersRokScheduleRepository = usersRokScheduleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UsersWorkScheduleDTO>> Execute(int scheduleId, CancellationToken cancellationToken)
        {
            if (scheduleId <= 0)
            {
                throw new ArgumentException("Wrong user schedule Id");
            }

            var getSchedule = await _usersRokScheduleRepository.Get(scheduleId, cancellationToken).ConfigureAwait(false);
            var resultDTO = _mapper.Map<UsersWorkScheduleDTO>(getSchedule);

            return ServiceResult<UsersWorkScheduleDTO>.SuccessResult(resultDTO);
        }

        public async Task<ServiceResult<UsersWorkScheduleDTO>> Execute(int userId, string weekday, CancellationToken cancellationToken)
        {
            if (userId <= 0) { throw new ArgumentException("Wrong user Id"); }

            var getSchedule = await _usersRokScheduleRepository
                 .GetIncluding(x => x.WeekWorkingDay)
                 .Where(x => x.UserId == userId && x.WeekWorkingDay.Name == weekday)
                 .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            var resultDTO = _mapper.Map<UsersWorkScheduleDTO>(getSchedule);
            return ServiceResult<UsersWorkScheduleDTO>.SuccessResult(resultDTO);

        }
        public async Task<ServiceResult<UsersWorkScheduleDTO>> Execute(int userId, int weekdayId, CancellationToken cancellationToken)
        {
            if (userId <= 0) { throw new ArgumentException("Wrong user Id"); }

            var getSchedule = await _usersRokScheduleRepository
                 .Where(x => x.UserId == userId && x.WeekWorkingDayId == weekdayId)
                 .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            var resultDTO = _mapper.Map<UsersWorkScheduleDTO>(getSchedule);
            return ServiceResult<UsersWorkScheduleDTO>.SuccessResult(resultDTO);

        }
    }
}
