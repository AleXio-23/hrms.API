using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.UserProfile.CreateUserProfile
{
    public class CreateNewProfile : ICreateNewProfile
    {
        private readonly IRepository<Persistance.Entities.UserProfile> _profileRepository;

        public CreateNewProfile(IRepository<Persistance.Entities.UserProfile> profileRepository)
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
                RegisterDate = userPrfileDTO.RegisterDate
            };

            var addNewProfile = await _profileRepository.Add(newProfile, cancellationToken);
            return new ServiceResult<Persistance.Entities.UserProfile>()
            {
                Success = true,
                Data = addNewProfile,
                ErrorOccured = false
            };
        }
    }
}



