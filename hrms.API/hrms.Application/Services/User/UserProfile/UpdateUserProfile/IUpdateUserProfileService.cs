using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserProfile.UpdateUserProfile
{
    public interface IUpdateUserProfileService
    {
        Task<ServiceResult<Persistance.Entities.UserProfile>> Execute(UserProfileDTO userPrfileDTO, CancellationToken cancellationToken);
    }
}
