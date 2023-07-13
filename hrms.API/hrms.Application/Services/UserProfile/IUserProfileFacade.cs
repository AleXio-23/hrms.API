using hrms.Application.Services.UserProfile.CreateUserProfile;
using hrms.Application.Services.UserProfile.UpdateUserProfile;

namespace hrms.Application.Services.UserProfile
{
    public interface IUserProfileFacade
    {
        ICreateNewProfileService CreateNewProfile { get; }
        IUpdateUserProfileService UpdateUserProfileService { get; }
    }
}
