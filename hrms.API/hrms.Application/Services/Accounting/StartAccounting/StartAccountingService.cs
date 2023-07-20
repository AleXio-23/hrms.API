using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.StartAccounting
{
    public class StartAccountingService : IStartAccountingService
    {
        private readonly IRepository<TraceWorking> _traceWorkingRepositroy;
        private readonly IRepository<WorkingTraceReport> _workingTraceReportRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IRepository<EventNameTypeLookup> _eventTypeNameLookupRepository;

        public StartAccountingService(IRepository<TraceWorking> traceWorkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IGetCurrentUserIdService getCurrentUserIdService, IRepository<EventNameTypeLookup> eventTypeNameLookupRepository)
        {
            _traceWorkingRepositroy = traceWorkingRepositroy;
            _workingTraceReportRepository = workingTraceReportRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
            _eventTypeNameLookupRepository = eventTypeNameLookupRepository;
        }

        public async Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken)
        {

            var userId = _getCurrentUserIdService.Execute();
            //Check if work already started today and not finished yet
            var getResult = await _workingTraceReportRepository
                .FirstOrDefaultAsync(x => x.UserId == _getCurrentUserIdService.Execute()
                                        && x.WorkStarted.HasValue
                                        && x.WorkStarted.Value.Date == DateTime.Today.Date, cancellationToken);
            if (getResult != null)
            {
                throw new ArgumentException($"User {userId} already started work today");
            }
            //first make record in report
            var newReport = new WorkingTraceReport()
            {
                UserId = userId
            };

            var getJobStartId = await _eventTypeNameLookupRepository.FirstOrDefaultAsync(x => x.EventName == "Job" && x.EventType == "Start", cancellationToken) ?? throw new NotFoundException($"Job Start type not found");
            var newReportRecord = await _workingTraceReportRepository.Add(newReport, cancellationToken);

            var createNewTraceRecord = new TraceWorking()
            {
                WorkingTraceId = newReportRecord.Id,
                EventNameTypeId = getJobStartId.Id
            };

            var newTraceRecordAddResult = await _traceWorkingRepositroy.Add(createNewTraceRecord, cancellationToken);

            newReport.WorkStarted = newTraceRecordAddResult.EventOccurTime;
            await _workingTraceReportRepository.Update(newReport, cancellationToken);

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
