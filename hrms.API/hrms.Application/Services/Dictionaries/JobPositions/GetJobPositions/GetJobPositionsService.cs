using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.JobPositions.GetJobPositions
{
    public class GetJobPositionsService : IGetJobPositionsService
    {
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public GetJobPositionsService(IRepository<JobPosition> jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<ServiceResult<List<JobPositionDTO>>> Execute(JobPositionFilter filter, CancellationToken cancellationToken)
        {
            var query = _jobPositionRepository.GetAllAsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }
            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }

            var result = await query.Select(x => new JobPositionDTO()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            }).ToListAsync(cancellationToken);

            return new ServiceResult<List<JobPositionDTO>>()
            {
                Success = true,
                ErrorOccured = false,
                Data = result ?? new List<JobPositionDTO>()
            };
        }
    }
}
