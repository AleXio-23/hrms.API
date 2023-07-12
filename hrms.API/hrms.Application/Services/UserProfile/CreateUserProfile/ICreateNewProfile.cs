using hrms.Domain.Models.User;
using hrms.Shared.Models;

namespace hrms.Application.Services.UserProfile.CreateUserProfile
{
    public interface ICreateNewProfile
    {
        Task<ServiceResult<Persistance.Entities.UserProfile>> Execute(UserProfileDTO userPrfileDTO, CancellationToken cancellationToken);
    }
}
