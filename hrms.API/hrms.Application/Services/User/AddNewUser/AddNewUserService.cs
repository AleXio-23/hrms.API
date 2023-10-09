using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Domain.Models.Auth;
using hrms.Domain.Models.User.AddNewUser;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.AddNewUser
{
    public class AddNewUserService : IAddNewUserService
    {
        private readonly IRegisterService _registeredServices;

        public AddNewUserService(IRegisterService registeredServices)
        {
            _registeredServices = registeredServices;
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

            var registerResult = await _registeredServices.Execute(registerUser, cancellationToken).ConfigureAwait(false);

            if (registerResult != null && registerResult.Success && registerResult.Data > 0)
            {
                var userProfile = new Persistance.Entities.UserProfile()
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
            }


            return ServiceResult<int>.SuccessResult(-1);
        }
    }
}
