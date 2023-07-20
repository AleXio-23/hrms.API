using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.WorkOnLateLog.AddWorkOnLateLog
{
    public class AddWorkOnLateLogService : IAddWorkOnLateLogService
    {
        private readonly IRepository<Persistance.Entities.WorkOnLateLog> _workOnLateLogRepository;
        private readonly IMapper _mapper;

        public AddWorkOnLateLogService(IRepository<Persistance.Entities.WorkOnLateLog> workOnLateLogRepository, IMapper mapper)
        {
            _workOnLateLogRepository = workOnLateLogRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WorkOnLateLogDTO>> Execute(WorkOnLateLogDTO workOnLateLogDTO, CancellationToken cancellationToken)
        {
            var newWorkOnlte = new Persistance.Entities.WorkOnLateLog()
            {
                UserId = workOnLateLogDTO.UserId,
                WorkingTraceReportId = workOnLateLogDTO.WorkingTraceReportId
            };

            var result = await _workOnLateLogRepository.Add(newWorkOnlte, cancellationToken).ConfigureAwait(false);
            var resultDTO = _mapper.Map<WorkOnLateLogDTO>(result);

            return ServiceResult<WorkOnLateLogDTO>.SuccessResult(resultDTO);
        }
    }
}
