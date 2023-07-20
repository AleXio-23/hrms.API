using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLog
{
    public class GetWorkOnLateLogService : IGetWorkOnLateLogService
    {
        private readonly IRepository<Persistance.Entities.WorkOnLateLog> _workOnLateLogReposiory;
        private readonly IMapper _mapper;

        public GetWorkOnLateLogService(IRepository<Persistance.Entities.WorkOnLateLog> workOnLateLogReposiory, IMapper mapper)
        {
            _workOnLateLogReposiory = workOnLateLogReposiory;
            _mapper = mapper;
        }

        public async Task<ServiceResult<WorkOnLateLogDTO>> Execute(int logId, CancellationToken cancellationToken)
        {
            var getWorkingLog = await _workOnLateLogReposiory.Get(logId, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Working late log on id {logId} not found");

            var getMappedDto = _mapper.Map<WorkOnLateLogDTO>(getWorkingLog);

            return ServiceResult<WorkOnLateLogDTO>.SuccessResult(getMappedDto);
        }
    }
}
