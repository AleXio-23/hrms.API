using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserProfile.UpdateUserProfile
{
    public class UpdateUserProfileService : IUpdateUserProfileService
    {
        private readonly IRepository<Persistance.Entities.UserProfile> _profileRepository;

        public UpdateUserProfileService(IRepository<Persistance.Entities.UserProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<ServiceResult<Persistance.Entities.UserProfile>> Execute(UserProfileDTO userPrfileDTO, CancellationToken cancellationToken)
        {
            var getExistingProfile = await _profileRepository.FirstOrDefaultAsync(x => x.UserId == userPrfileDTO.UserId, cancellationToken)
                                        ?? throw new NotFoundException($"Profile for user: {userPrfileDTO.UserId} not found.");

            getExistingProfile.BirthDate = userPrfileDTO.BirthDate;
            getExistingProfile.Firstname = userPrfileDTO.Firstname;
            getExistingProfile.Lastname = userPrfileDTO.Lastname;
            getExistingProfile.PhoneNumber1 = userPrfileDTO.PhoneNumber1;
            getExistingProfile.PhoneNumber2 = userPrfileDTO.PhoneNumber2;
            getExistingProfile.GenderId = userPrfileDTO.GenderId;
            getExistingProfile.PersonalNumber = userPrfileDTO.PersonalNumber;

            var saveResult = await _profileRepository.Update(getExistingProfile, cancellationToken).ConfigureAwait(false);

            return new ServiceResult<Persistance.Entities.UserProfile>()
            {
                Success = true,
                ErrorOccured = false,
                Data = saveResult
            };
        }
    }
}
