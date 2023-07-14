using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserJobPosition.DeleteUserJobPosition
{
    public interface IDeleteUserJobPositionService
    {
        Task<ServiceResult<bool>> Execute(int userId, CancellationToken cancellationToken);
    }
}
