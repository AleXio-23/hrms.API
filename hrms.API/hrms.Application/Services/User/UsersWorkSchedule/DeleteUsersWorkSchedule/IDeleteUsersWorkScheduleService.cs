using hrms.Shared.Models;

namespace hrms.Application.Services.User.UsersWorkSchedule.DeleteUsersWorkSchedule
{
    public interface IDeleteUsersWorkScheduleService
    {
        Task<ServiceResult<bool>> Execute(int scheduleId, CancellationToken cancellationToken);
    }
}
