using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.GetJobPosition
{
    public interface IGetJobPositionService
    {
        Task<ServiceResult<JobPositionDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
