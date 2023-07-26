using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules
{
    public class GetUsersWorkSchedulesService : IGetUsersWorkSchedulesService
    {
        private readonly IRepository<Persistance.Entities.UsersWorkSchedule> _usersRokScheduleRepository;
        private readonly IMapper _mapper;

        public GetUsersWorkSchedulesService(IRepository<Persistance.Entities.UsersWorkSchedule> usersRokScheduleRepository, IMapper mapper)
        {
            _usersRokScheduleRepository = usersRokScheduleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<UsersWorkScheduleDTO>>> Execute(UsersWorkScheduleFilter filter, CancellationToken cancellationToken)
        {
            var query = _usersRokScheduleRepository.GetAllAsQueryable();
            if (filter.UserId != null)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }
            if (filter.WeekWorkingDayId != null)
            {
                query = query.Where(x => x.WeekWorkingDayId == filter.WeekWorkingDayId);
            }

            var resultDTO = await query
                .Select(x => _mapper.Map<UsersWorkScheduleDTO>(x))
                .ToListAsync(cancellationToken).ConfigureAwait(false) ?? new List<UsersWorkScheduleDTO>();

            return ServiceResult<List<UsersWorkScheduleDTO>>.SuccessResult(resultDTO);
        }
    }
}
