using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Application.Services.User.UserProfile.CreateUserProfile;
using hrms.Domain.Models.Auth;
using hrms.Domain.Models.User;
using hrms.Domain.Models.User.AddNewUser;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.AddNewUser
{
    public class AddNewUserService : IAddNewUserService
    {
        private readonly IRegisterService _registeredServices;
        private readonly ICreateNewProfileService _createNewProfileService;
        private readonly IRepository<UserLocation> _userLocationsRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Persistance.Entities.UserJobPosition> _userJobPositionRepository;
        private readonly IRepository<Persistance.Entities.UsersWorkSchedule> _userWorkingScheduleRepository;

        public AddNewUserService(IRegisterService registeredServices, ICreateNewProfileService createNewProfileService, IRepository<UserLocation> userLocationsRepository, IRepository<UserRole> userRoleRepository, IRepository<Persistance.Entities.UserJobPosition> userJobPositionRepository, IRepository<Persistance.Entities.UsersWorkSchedule> userWorkingScheduleRepository)
        {
            _registeredServices = registeredServices;
            _createNewProfileService = createNewProfileService;
            _userLocationsRepository = userLocationsRepository;
            _userRoleRepository = userRoleRepository;
            _userJobPositionRepository = userJobPositionRepository;
            _userWorkingScheduleRepository = userWorkingScheduleRepository;
        }

        public async Task<ServiceResult<int>> Execute(AddNewUserRequest request, CancellationToken cancellationToken)
        {
            var password = "asdASD123!@#";
            var userEmail = request.Email;

            var registerUser = new RegisterDto()
            {
                Email = userEmail,
                Password = password,
                RepeatPassword = password
            };
            //TODO - დარეიგსტირებულს იუზერს უნდა გაეგზავნოს მეილი - ტექსტით, რეგისტრირებული იუზერ/მეილი + პაროლი
            var registerResult = await _registeredServices.Execute(registerUser, cancellationToken).ConfigureAwait(false);

            if (registerResult != null && registerResult.Success && registerResult.Data > 0)
            {
                var userProfile = new UserProfileDTO()
                {
                    UserId = registerResult.Data,
                    Firstname = request.PersonalInfo?.Firstname,
                    Lastname = request.PersonalInfo?.Lastname,
                    PhoneNumber1 = request.Phone,
                    PhoneNumber2 = request.Phone2,
                    BirthDate = request.PersonalInfo?.BirthDate,
                    PersonalNumber = request.PersonalInfo?.PersonalNumber,
                    GenderId = request.PersonalInfo?.GenderId,
                };
                await _createNewProfileService.Execute(userProfile, cancellationToken).ConfigureAwait(false);


                var userLocation = new UserLocation()
                {
                    UserId = registerResult.Data,
                    CountryId = request.PersonalInfo?.CountryId,
                    StateId = request.PersonalInfo?.StateId,
                    CityId = request.PersonalInfo?.CityId,
                    Address = request.Address
                };

                await _userLocationsRepository.Add(userLocation, cancellationToken).ConfigureAwait(false);
                if (request?.Position?.RoleId != null && request.Position.RoleId > 0)
                {
                    var userRole = new UserRole()
                    {
                        UserId = registerResult.Data,
                        RoleId = request?.Position?.RoleId ?? -1
                    };

                    await _userRoleRepository.Add(userRole, cancellationToken).ConfigureAwait(false);
                }


                var userJobPosition = new Persistance.Entities.UserJobPosition()
                {
                    UserId = registerResult.Data,
                    PositionId = request?.Position?.JobPositionId,
                    DepartmentId = request?.Position?.DepartmentId
                };
                await _userJobPositionRepository.Add(userJobPosition, cancellationToken).ConfigureAwait(false);

                if (request?.WorkingSchedules.Count > 0)
                {
                    foreach (var item in request.WorkingSchedules)
                    {
                        if (item.WeekDayId != null)
                        {
                            var newUserWorkSchedule = new Persistance.Entities.UsersWorkSchedule()
                            {
                                UserId = registerResult.Data,
                                WeekWorkingDayId = item.WeekDayId ?? -1,
                                StartTime = item.TimeFrom,
                                EndTime = item.TimeTo
                            };

                            await _userWorkingScheduleRepository.Add(newUserWorkSchedule, cancellationToken).ConfigureAwait(false);
                        }

                    }
                }
                return ServiceResult<int>.SuccessResult(registerResult.Data);
            }

            return ServiceResult<int>.ErrorResult("UNKNOW_ERROR_OCCURED");
        }
    }

}
