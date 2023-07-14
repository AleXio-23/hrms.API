using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserJobPosition.GetUserJobPosition
{
    public interface IGetUserJobPositionService
    {
        Task<ServiceResult<UserJobPositionDTO>> Execute(int userId, CancellationToken cancellationToken);
    }
}
