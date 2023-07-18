using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserProfile.CreateUserProfile
{
    public class CreateNewProfileService : ICreateNewProfileService
    {
        private readonly IRepository<Persistance.Entities.UserProfile> _profileRepository;

        public CreateNewProfileService(IRepository<Persistance.Entities.UserProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<ServiceResult<Persistance.Entities.UserProfile>> Execute(UserProfileDTO userPrfileDTO, CancellationToken cancellationToken)
        {
            var newProfile = new Persistance.Entities.UserProfile()
            {
                UserId = userPrfileDTO.UserId ?? throw new ArgumentException("User Id is incorrect"),
                Firstname = userPrfileDTO.Firstname,
                Lastname = userPrfileDTO.Lastname,
                PhoneNumber1 = userPrfileDTO.PhoneNumber1,
                PhoneNumber2 = userPrfileDTO.PhoneNumber2,
                BirthDate = userPrfileDTO.BirthDate,
                PersonalNumber = userPrfileDTO.PersonalNumber,
                GenderId = userPrfileDTO.GenderId,
                RegisterDate = DateTime.Now
            };

            var addNewProfile = await _profileRepository.Add(newProfile, cancellationToken).ConfigureAwait(false);

            return ServiceResult<Persistance.Entities.UserProfile>.SuccessResult(addNewProfile);
        }
    }
}



