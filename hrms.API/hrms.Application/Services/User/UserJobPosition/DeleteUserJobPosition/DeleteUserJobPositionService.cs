using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserJobPosition.DeleteUserJobPosition
{
    public class DeleteUserJobPositionService : IDeleteUserJobPositionService
    {
        private readonly IRepository<Persistance.Entities.UserJobPosition> _userJobPositionRepository;

        public DeleteUserJobPositionService(IRepository<Persistance.Entities.UserJobPosition> userJobPositionRepository)
        {
            _userJobPositionRepository = userJobPositionRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int userId, CancellationToken cancellationToken)
        {
            if (!(await _userJobPositionRepository.AnyAsync(x => x.UserId == userId, cancellationToken)))
            {
                throw new NotFoundException("User job position record not found");
            }

            await _userJobPositionRepository.Delete(x => x.UserId == userId, cancellationToken).ConfigureAwait(false);

            return new ServiceResult<bool>()
            {
                Success = true,
                ErrorOccured = true,
                Data = true
            };
        }
    }
}
