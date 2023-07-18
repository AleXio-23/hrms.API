using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.DeleteJobPosition
{
    public class DeleteJobPositionService : IDeleteJobPositionService
    {
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public DeleteJobPositionService(IRepository<JobPosition> jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1) throw new ArgumentException("Wrong value for id");
            var getJobPosition = await _jobPositionRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"JobPosition on id: {id} not found");
            getJobPosition.IsActive = false;
            await _jobPositionRepository.Update(getJobPosition, cancellationToken).ConfigureAwait(false);

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
