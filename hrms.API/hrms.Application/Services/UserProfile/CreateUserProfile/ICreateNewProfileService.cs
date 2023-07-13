using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.UserProfile.CreateUserProfile
{
    public interface ICreateNewProfileService
    {
        Task<ServiceResult<Persistance.Entities.UserProfile>> Execute(UserProfileDTO userPrfileDTO, CancellationToken cancellationToken);
    }
}
