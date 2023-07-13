using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.UserProfile.UpdateUserProfile
{
    public interface IUpdateUserProfileService
    {
        Task<ServiceResult<Persistance.Entities.UserProfile>> Execute(UserProfileDTO userPrfileDTO, CancellationToken cancellationToken);
    }
}
