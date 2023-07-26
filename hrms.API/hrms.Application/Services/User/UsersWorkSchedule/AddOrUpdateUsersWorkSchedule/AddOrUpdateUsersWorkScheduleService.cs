using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UsersWorkSchedule.AddOrUpdateUsersWorkSchedule
{
    public class AddOrUpdateUsersWorkScheduleService : IAddOrUpdateUsersWorkScheduleService
    {
        private readonly IRepository<Persistance.Entities.UsersWorkSchedule> _usersRokScheduleRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateUsersWorkScheduleService(IRepository<Persistance.Entities.UsersWorkSchedule> usersRokScheduleRepository, IMapper mapper)
        {
            _usersRokScheduleRepository = usersRokScheduleRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UsersWorkScheduleDTO>> Execute(UsersWorkScheduleDTO usersWorkScheduleDTO, CancellationToken cancellationToken)
        {
            if (usersWorkScheduleDTO.Id == null || usersWorkScheduleDTO.Id <= 0)
            {
                var newWorkSchedule = new Persistance.Entities.UsersWorkSchedule()
                {
                    UserId = usersWorkScheduleDTO.UserId ?? throw new ArgumentException("Wrong userId"),
                    WeekWorkingDayId = usersWorkScheduleDTO.WeekWorkingDayId ?? throw new ArgumentException("Wrong Week working day Id"),
                    StartTime = usersWorkScheduleDTO.StartTime ?? throw new ArgumentException("Start time must be provided"),
                    EndTime = usersWorkScheduleDTO.EndTime ?? throw new ArgumentException("End time must be provided")
                };

                var result = await _usersRokScheduleRepository.Add(newWorkSchedule, cancellationToken).ConfigureAwait(false);
                var mappedResult = _mapper.Map<UsersWorkScheduleDTO>(result);

                return ServiceResult<UsersWorkScheduleDTO>.SuccessResult(mappedResult);
            }
            else
            {
                var getExistingSchedule = await _usersRokScheduleRepository
                    .Get(usersWorkScheduleDTO.Id ?? -1, cancellationToken)
                    .ConfigureAwait(false) ?? throw new NotFoundException($"User work schedule on id {usersWorkScheduleDTO.Id} not found");

                getExistingSchedule.WeekWorkingDayId = usersWorkScheduleDTO.WeekWorkingDayId ?? throw new ArgumentException("Wrong Week working day Id");
                getExistingSchedule.StartTime = usersWorkScheduleDTO.StartTime ?? throw new ArgumentException("Start time must be provided");
                getExistingSchedule.EndTime = usersWorkScheduleDTO.EndTime ?? throw new ArgumentException("End time must be provided");

                await _usersRokScheduleRepository.Update(getExistingSchedule, cancellationToken).ConfigureAwait(false);
                return ServiceResult<UsersWorkScheduleDTO>.SuccessResult(usersWorkScheduleDTO);
            }
        }
    }
}
