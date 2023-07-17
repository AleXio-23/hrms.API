using AutoMapper;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.GetUser
{
    public class GetUserService : IGetUserService
    {
        private readonly IRepository<Persistance.Entities.User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserService(IRepository<Persistance.Entities.User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserDTO>> Execute(int userId, CancellationToken cancellationToken)
        {
            if (userId < 1) { throw new ArgumentException("Wrong user id value"); }
#pragma warning disable CS8603 // Possible null reference return.
            var getUser = await _userRepository
                       .GetIncluding(x => x.UserProfile)
                       .Select(x => new UserDTO()
                       {
                           Id = x.Id,
                           Username = x.Username,
                           Email = x.Email,
                           UserProfileDTO = x.UserProfile == null
                               ? null
                               : _mapper.Map<UserProfileDTO>(x.UserProfile)
                       })
                       .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
                       .ConfigureAwait(false) ?? throw new NotFoundException($"User on id: {userId} not found");
#pragma warning restore CS8603 // Possible null reference return.

            return ServiceResult<UserDTO>.SuccessResult(getUser);
        }
    }
}
