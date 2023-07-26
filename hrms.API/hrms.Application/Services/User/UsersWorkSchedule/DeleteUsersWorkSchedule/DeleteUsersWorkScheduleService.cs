using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UsersWorkSchedule.DeleteUsersWorkSchedule
{
    public class DeleteUsersWorkScheduleService : IDeleteUsersWorkScheduleService
    {
        private readonly IRepository<Persistance.Entities.UsersWorkSchedule> _usersRokScheduleRepository;

        public DeleteUsersWorkScheduleService(IRepository<Persistance.Entities.UsersWorkSchedule> usersRokScheduleRepository)
        {
            _usersRokScheduleRepository = usersRokScheduleRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int scheduleId, CancellationToken cancellationToken)
        {
            if (scheduleId <= 0)
            {
                throw new ArgumentException("Wrong user schedule id value");
            }
            await _usersRokScheduleRepository.Delete(scheduleId, cancellationToken).ConfigureAwait(false);
            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
