using hrms.Domain.Models.Auth;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace hrms.Infranstructure.Auth.Register
{
    public class RegisterService : IRegisterService
    {
        private readonly IRepository<User> _userRepository;

        public RegisterService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<string>> Execute(RegisterDto registerDto, CancellationToken cancellationToken)
        {

            if (!IsValidEmail(registerDto.Email))
            {
                throw new ValidationException("Wrong email format");
            }

            var userName = registerDto.Email;

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                throw new ArgumentException("Passwords don't match");
            }
            if (await UserExists(userName))
            {
                throw new RecordExistsException("Username is registered");
            }
            if (await UserWithEmailExists(registerDto.Email))
            {
                throw new RecordExistsException("This email is registered");
            }

            var hmac = new HMACSHA512();
            var newUser = new User()
            {
                Username = userName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _userRepository.Add(newUser, cancellationToken);

            return new ServiceResult<string>()
            {
                Success = true,
                Data = newUser.Email + " Added"
            };
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
            return await _userRepository.AnyAsync(x => x.Username.ToLower() == username.ToLower());
        }
        private async Task<bool> UserWithEmailExists(string email)
        {
            return await _userRepository.AnyAsync(x => x.Email == email);
        }

    }
}
