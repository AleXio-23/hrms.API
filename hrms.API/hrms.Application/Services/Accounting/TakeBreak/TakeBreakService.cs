using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Enums;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using hrms.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Accounting.TakeBreak
{
    public class TakeBreakService : ITakeBreakService
    {
        private readonly IRepository<TraceWorking> _traceWorkingRepositroy;
        private readonly IRepository<WorkingTraceReport> _workingTraceReportRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IRepository<EventNameTypeLookup> _eventTypeNameLookupRepository;
        private readonly IRepository<WorkingStatus> _workingStatusRepository;

        public TakeBreakService(IRepository<TraceWorking> traceWorkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IGetCurrentUserIdService getCurrentUserIdService, IRepository<EventNameTypeLookup> eventTypeNameLookupRepository, IRepository<WorkingStatus> workingStatusRepository)
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
                                      && x.WorkStarted.Value.Date == DateTime.Today.Date, cancellationToken);

            if (getResult == null || (getResult != null & getResult?.WorkEnded != null))
            {
                throw new ArgumentException($"User {userId} not started work yet.");
            }
            if (getResult != null && getResult.WorkEnded == null)
            {
                var getCurrentStatus = await _workingStatusRepository.FirstOrDefaultAsync(x => x.Code == EnumDescription.GetDescription(CurrentStatusEnums.BREAK), cancellationToken) ?? throw new NotFoundException("Break status not found");
                var getAllTodaysTraces = await _traceWorkingRepositroy.Where(x => x.WorkingTraceId == getResult.Id)
                                                                    .OrderByDescending(x => x.EventOccurTime)
                                                                    .ToListAsync(cancellationToken);
                var getBreakStartStatus = await _eventTypeNameLookupRepository.FirstOrDefaultAsync(x => x.EventName == "Break" && x.EventType == "Start", cancellationToken) ?? throw new NotFoundException($"Break End type not found");
                if (getAllTodaysTraces.Count > 0 && getAllTodaysTraces[0].EventNameTypeId == getBreakStartStatus.Id)
                {
                    throw new ArgumentException($"User {userId} is already on break");
                }

                //create new trace record
                var createNewTraceRecord = new TraceWorking()
                {
                    WorkingTraceId = getResult.Id,
                    EventNameTypeId = getBreakStartStatus.Id
                };

                var newTraceRecordAddResult = await _traceWorkingRepositroy.Add(createNewTraceRecord, cancellationToken);

                getResult.CurrentStatusId = getCurrentStatus.Id;
                await _workingTraceReportRepository.Update(getResult, cancellationToken);
                return ServiceResult<bool>.SuccessResult(true);
            }

            throw new Exception("Unexpected error occured on taking break");
        }
    }
}
