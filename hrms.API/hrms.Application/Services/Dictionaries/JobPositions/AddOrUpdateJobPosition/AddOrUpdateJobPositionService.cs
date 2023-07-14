using AutoMapper;
using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.JobPositions.AddOrUpdateJobPosition
{
    public class AddOrUpdateJobPositionService : IAddOrUpdateJobPositionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<JobPosition> _jobPositionRepository;

        public AddOrUpdateJobPositionService(IMapper mapper, IRepository<JobPosition> jobPositionRepository)
        {
            _mapper = mapper;
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<ServiceResult<JobPositionDTO>> Execute(JobPositionDTO jobPositionDTO, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(jobPositionDTO.Name)) throw new ArgumentException("Job position name can't be null or empty");
            if (jobPositionDTO.Id == null || jobPositionDTO.Id < 1)
            {
                var newJobPosition = _mapper.Map<JobPosition>(jobPositionDTO);
                var addResult = await _jobPositionRepository.Add(newJobPosition, cancellationToken).ConfigureAwait(false);
                var returnResult = _mapper.Map<JobPositionDTO>(addResult);

                return new ServiceResult<JobPositionDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = returnResult
                };
            }
            else if (jobPositionDTO.Id > 1)
            {
                var getJobPosition = await _jobPositionRepository.Get(jobPositionDTO.Id ?? -1, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record on Id:{jobPositionDTO.Id} not found");
                getJobPosition.Name = jobPositionDTO.Name;
                getJobPosition.IsActive = jobPositionDTO.IsActive;

                var saveResult = await _jobPositionRepository.Update(getJobPosition, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<JobPositionDTO>(saveResult);
                return new ServiceResult<JobPositionDTO>()
                {
                    Success = true,
                    ErrorOccured = false,
                    Data = resultDto
                }; 
            }
            else
            {
                throw new Exception("Unexpected error occured");
            }
        }
    }
}
