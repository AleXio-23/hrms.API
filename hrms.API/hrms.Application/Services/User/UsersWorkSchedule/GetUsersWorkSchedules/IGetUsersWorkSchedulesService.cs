using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules
{
    public interface IGetUsersWorkSchedulesService
    {
        Task<ServiceResult<List<UsersWorkScheduleDTO>>> Execute(UsersWorkScheduleFilter filter, CancellationToken cancellationToken);
    }
}
