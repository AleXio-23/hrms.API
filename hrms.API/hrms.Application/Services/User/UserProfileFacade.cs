using hrms.Application.Services.User.UserJobPosition.AddOrUpdateUserJobPosition;
using hrms.Application.Services.User.UserJobPosition.DeleteUserJobPosition;
using hrms.Application.Services.User.UserJobPosition.GetUserJobPosition;
using hrms.Application.Services.User.UserProfile.CreateUserProfile;
using hrms.Application.Services.User.UserProfile.UpdateUserProfile;

namespace hrms.Application.Services.UserProfile
{
    public class UserProfileFacade : IUserProfileFacade
    {
        public UserProfileFacade(ICreateNewProfileService createNewProfile, IUpdateUserProfileService updateUserProfileService, IAddOrUpdateUserJobPositionService addOrUpdateUserJobPositionService, IDeleteUserJobPositionService deleteUserJobPositionService, IGetUserJobPositionService getUserJobPositionService)
        {
            CreateNewProfile = createNewProfile;
            UpdateUserProfileService = updateUserProfileService;
            AddOrUpdateUserJobPositionService = addOrUpdateUserJobPositionService;
            DeleteUserJobPositionService = deleteUserJobPositionService;
            GetUserJobPositionService = getUserJobPositionService;
        }

        public ICreateNewProfileService CreateNewProfile { get; }
        public IUpdateUserProfileService UpdateUserProfileService { get; }
        public IAddOrUpdateUserJobPositionService AddOrUpdateUserJobPositionService { get; }
        public IDeleteUserJobPositionService DeleteUserJobPositionService { get; }
        public IGetUserJobPositionService GetUserJobPositionService { get; }
    }
}
