using hrms.Domain.Models.User;

namespace hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules
{
    public interface IGetUsersWorkSchedulesService
    {
        Task<UsersWorkScheduleDTO> Execute(UsersWorkScheduleFilter filter, CancellationToken cancellationToken);
    }
}
