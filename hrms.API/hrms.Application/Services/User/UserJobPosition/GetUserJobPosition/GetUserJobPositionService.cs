using AutoMapper;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.User.UserJobPosition.GetUserJobPosition
{
    public class GetUserJobPositionService : IGetUserJobPositionService
    {

        private readonly IRepository<Persistance.Entities.UserJobPosition> _userJobPositionRepository;
        private readonly IMapper _mapper;

        public GetUserJobPositionService(IRepository<Persistance.Entities.UserJobPosition> userJobPositionRepository, IMapper mapper)
        {
            _userJobPositionRepository = userJobPositionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserJobPositionDTO>> Execute(int userId, CancellationToken cancellationToken)
        {
            var getJobPosition = (await _userJobPositionRepository
                 .GetIncluding(x => x.Position!, x => x.Department!)
                 .Where(x => x.UserId == userId)
                 .FirstOrDefaultAsync(cancellationToken)) ?? throw new ArgumentException($"Job position record for userId: {userId} not found");

            var jobPosition = getJobPosition.Position != null ? _mapper.Map<JobPositionDTO>(getJobPosition.Position) : null;
            var department = _mapper.Map<DepartmentDTO>(getJobPosition.Department ?? new Department());
            var userJobPosition = _mapper.Map<UserJobPositionDTO>(getJobPosition);

            userJobPosition.Position = jobPosition;
            userJobPosition.Department = department;

            return ServiceResult<UserJobPositionDTO>.SuccessResult(userJobPosition);
        }
    }
}
