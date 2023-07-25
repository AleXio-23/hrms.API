using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UsersWorkSchedule.AddOrUpdateUsersWorkSchedule
{
    public interface IAddOrUpdateUsersWorkScheduleService
    {
        Task<ServiceResult<UsersWorkScheduleDTO>> Execute(UsersWorkScheduleDTO usersWorkScheduleDTO, CancellationToken cancellationToken);
    }
}
