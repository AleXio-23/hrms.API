using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.DeleteJobPosition
{
    public interface IDeleteJobPositionService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
    }
}
