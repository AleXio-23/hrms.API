using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedule
{
    public interface IGetUsersWorkScheduleService
    {
        Task<ServiceResult<UsersWorkScheduleDTO>> Execute(int scheduleId, CancellationToken cancellationToken);
        Task<ServiceResult<UsersWorkScheduleDTO>> Execute(int userId, string weekday, CancellationToken cancellationToken);
        Task<ServiceResult<UsersWorkScheduleDTO>> Execute(int userId, int weekdayId, CancellationToken cancellationToken);
    }
}
