using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserJobPosition.AddOrUpdateUserJobPosition
{
    public interface IAddOrUpdateUserJobPositionService
    {
        Task<ServiceResult<UserJobPositionDTO>> Execute(UserJobPositionDTO userJobPositionDTO, CancellationToken cancellationToken);
    }
}
