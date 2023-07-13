using AutoMapper;
using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.GetJobPosition
{
    public class GetJobPositionService : IGetJobPositionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public GetJobPositionService(IMapper mapper, IRepository<JobPosition> jobPositionRepository)
        {
            _mapper = mapper;
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<ServiceResult<JobPositionDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                throw new ArgumentException("Wrong id value");
            }

            var getJobPosition = await _jobPositionRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"JobPosition with Id: {id} not found.");

            var jobPositionDto = _mapper.Map<JobPositionDTO>(getJobPosition);

            return new ServiceResult<JobPositionDTO>()
            {
                Success = true,
                ErrorOccured = false,
                Data = jobPositionDto
            };
        }
    }
}
