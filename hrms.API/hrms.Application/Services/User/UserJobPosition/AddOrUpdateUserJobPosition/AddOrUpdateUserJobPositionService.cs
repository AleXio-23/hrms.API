using hrms.Domain.Models.User;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.User.UserJobPosition.AddOrUpdateUserJobPosition
{
    public class AddOrUpdateUserJobPositionService : IAddOrUpdateUserJobPositionService
    {
        private readonly IRepository<Persistance.Entities.UserJobPosition> _userJobPositionRepository;

        public AddOrUpdateUserJobPositionService(IRepository<Persistance.Entities.UserJobPosition> userJobPositionRepository)
        {
            _userJobPositionRepository = userJobPositionRepository;
        }

        public async Task<ServiceResult<UserJobPositionDTO>> Execute(UserJobPositionDTO userJobPositionDTO, CancellationToken cancellationToken)
        {
            var newJobPosition = new Persistance.Entities.UserJobPosition()
            {
                UserId = userJobPositionDTO.UserId ?? throw new ArgumentException("User id can't be null"),
                DepartmentId = userJobPositionDTO.DepartmentId ?? throw new ArgumentException("Department id can't be null"),
                PositionId = userJobPositionDTO.PositionId ?? throw new ArgumentException("Position id can't be null"),
            };

            var result = await _userJobPositionRepository.Add(newJobPosition, cancellationToken).ConfigureAwait(false);

            return ServiceResult<UserJobPositionDTO>.SuccessResult(userJobPositionDTO);
        }
    }
}
