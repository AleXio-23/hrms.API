using hrms.Application.Services.User.UserJobPosition.AddOrUpdateUserJobPosition;
using hrms.Application.Services.User.UserJobPosition.DeleteUserJobPosition;
using hrms.Application.Services.User.UserJobPosition.GetUserJobPosition;
using hrms.Application.Services.User.UserProfile.CreateUserProfile;
using hrms.Application.Services.User.UserProfile.UpdateUserProfile;

namespace hrms.Application.Services.UserProfile
{
    public interface IUserProfileFacade
    {
        ICreateNewProfileService CreateNewProfile { get; }
        IUpdateUserProfileService UpdateUserProfileService { get; }
        IAddOrUpdateUserJobPositionService AddOrUpdateUserJobPositionService { get; }
        IDeleteUserJobPositionService DeleteUserJobPositionService { get; }
        IGetUserJobPositionService GetUserJobPositionService { get; }
    }
}
