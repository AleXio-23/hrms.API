
using hrms.Domain.Models.Auth;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using hrms.Application.Infranstructure.Interfaces.UserInterfaces;

namespace hrms.Infranstructure.Auth.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly IRepository<User> _userRepository;

        public RegisterService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<int>> Execute(RegisterDto registerDto, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(registerDto.Email) || !IsValidEmail(registerDto.Email))
            {
                throw new ValidationException("Wrong email format");
            }
            if (string.IsNullOrEmpty(registerDto.Password))
            {
                throw new ValidationException("Wrong password");
            }

            var userName = registerDto.Email;

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                throw new ArgumentException("Passwords don't match");
            }
            if (await UserWithEmailExists(registerDto.Email).ConfigureAwait(false))
            {
                throw new RecordExistsException("This email is registered");
            }
            if (await UserExists(userName).ConfigureAwait(false))
            {
                throw new RecordExistsException("Username is registered");
            }


            var hmac = new HMACSHA512();
            var newUser = new User()
            {
                Username = userName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _userRepository.Add(newUser, cancellationToken).ConfigureAwait(false);

            return ServiceResult<int>.SuccessResult(newUser.Id);
        }
        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new(pattern);
            Match match = regex.Match(email);
            return match.Success;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userRepository.AnyAsync(x => x.Username.ToLower() == username.ToLower()).ConfigureAwait(false);
        }
        private async Task<bool> UserWithEmailExists(string email)
        {
            return await _userRepository.AnyAsync(x => x.Email == email).ConfigureAwait(false);
        }

    }
}
