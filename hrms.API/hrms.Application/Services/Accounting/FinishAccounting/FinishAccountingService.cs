using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Enums;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using hrms.Shared.Services;

namespace hrms.Application.Services.Accounting.FinishAccounting
{
    public class FinishAccountingService : IFinishAccountingService
    {
        private readonly IRepository<TraceWorking> _traceWorkingRepositroy;
        private readonly IRepository<WorkingTraceReport> _workingTraceReportRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IRepository<EventNameTypeLookup> _eventTypeNameLookupRepository;
        private readonly IRepository<WorkingStatus> _workingStatusRepository;

        public FinishAccountingService(IRepository<TraceWorking> traceWorkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IGetCurrentUserIdService getCurrentUserIdService, IRepository<EventNameTypeLookup> eventTypeNameLookupRepository, IRepository<WorkingStatus> workingStatusRepository)
        {
            _traceWorkingRepositroy = traceWorkingRepositroy;
            _workingTraceReportRepository = workingTraceReportRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
            _eventTypeNameLookupRepository = eventTypeNameLookupRepository;
            _workingStatusRepository = workingStatusRepository;
        }

        public async Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken)
        {
            var userId = _getCurrentUserIdService.Execute();
            var getResult = await _workingTraceReportRepository
                .FirstOrDefaultAsync(x => x.UserId == _getCurrentUserIdService.Execute()
                                        && x.WorkStarted.HasValue
                                        && x.WorkStarted.Value.Date == DateTime.Today.Date, cancellationToken) ?? throw new ArgumentException($"User {userId} not started work yet.");

            if (getResult != null && getResult.WorkEnded != null)
            {
                throw new ArgumentException($"User {userId} not started work yet.");
            }

            var getCurrentStauts = await _workingStatusRepository
               .FirstOrDefaultAsync(x => x.Code == EnumDescription.GetDescription(CurrentStatusEnums.FINISHED_WORK), cancellationToken)
               ?? throw new NotFoundException("Error getting finish work status");

            var getJobStartId = await _eventTypeNameLookupRepository.FirstOrDefaultAsync(x => x.EventName == "Job" && x.EventType == "End", cancellationToken) ?? throw new NotFoundException($"Job End type not found");

            var createNewTraceRecord = new TraceWorking()
            {
                WorkingTraceId = getResult?.Id ?? -1,
                EventNameTypeId = getJobStartId.Id
            };

            var newTraceRecordAddResult = await _traceWorkingRepositroy.Add(createNewTraceRecord, cancellationToken);
            if (getResult != null)
            {
                getResult.CurrentStatusId = getCurrentStauts?.Id;
                getResult.WorkEnded = newTraceRecordAddResult.EventOccurTime;
                await _workingTraceReportRepository.Update(getResult, cancellationToken);
            }

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
