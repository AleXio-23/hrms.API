using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.GetJobPositions
{
    public interface IGetJobPositionsService
    {
        Task<ServiceResult<List<JobPositionDTO>>> Execute(JobPositionFilter filter, CancellationToken cancellationToken);
    }
}
