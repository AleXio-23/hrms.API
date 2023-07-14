using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.AddOrUpdateJobPosition
{
    public interface IAddOrUpdateJobPositionService
    {
        Task<ServiceResult<JobPositionDTO>> Execute(JobPositionDTO jobPositionDTO, CancellationToken cancellationToken);
    }
}
