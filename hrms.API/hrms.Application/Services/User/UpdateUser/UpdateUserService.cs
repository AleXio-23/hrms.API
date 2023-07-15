using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UpdateUser
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IRepository<Persistance.Entities.User> _userRepository;

        public UpdateUserService(IRepository<Persistance.Entities.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<UserDTO>> Execute(UserDTO userDTO, CancellationToken cancellationToken)
        {
            if (userDTO.Id == null || userDTO.Id < 0) { throw new ArgumentException("Wrong user id value."); }

            var getUser = await _userRepository
                     .Get(userDTO.Id ?? -1, cancellationToken)
                     .ConfigureAwait(false) ?? throw new NotFoundException($"User on id: {userDTO.Id} not found");

            //update user without passwordChange
            if (string.IsNullOrEmpty(userDTO.OldPassword) && string.IsNullOrEmpty(userDTO.NewPassword) && string.IsNullOrEmpty(userDTO.NewPasswordConfirmation))
            {
                if (getUser.Username != userDTO.Username)
                {
                    if (await _userRepository.AnyAsync(x => x.Username == userDTO.Username, cancellationToken))
                    {
                        throw new ArgumentException("The username you entered is already in use. Please choose a different username.");
                    }
                }
                if (getUser.Email != userDTO.Email)
                {
                    if (await _userRepository.AnyAsync(x => x.Email == userDTO.Email, cancellationToken))
                    {
                        throw new ArgumentException("The username you entered is already in use. Please choose a different username.");
                    }
                }
                getUser.Username = userDTO.Username;
                getUser.Email = userDTO.Email;

                return new ServiceResult<UserDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = userDTO
                };
            }
            else
            {
                throw new Exception("პაროლის შეცვლა ჯერ არ დამატებულა");
            }

        }
    }
}