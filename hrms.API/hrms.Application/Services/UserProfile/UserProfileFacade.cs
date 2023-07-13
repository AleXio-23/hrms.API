using hrms.Application.Services.UserProfile.CreateUserProfile;
using hrms.Application.Services.UserProfile.UpdateUserProfile;

namespace hrms.Application.Services.UserProfile
{
    public class UserProfileFacade : IUserProfileFacade
    {
        public ICreateNewProfileService CreateNewProfile { get; } 
        public IUpdateUserProfileService UpdateUserProfileService { get; }

        public UserProfileFacade(ICreateNewProfileService createNewProfile, IUpdateUserProfileService updateUserProfileService)
        {
            CreateNewProfile = createNewProfile;
            UpdateUserProfileService = updateUserProfileService;
        }
    }
}
