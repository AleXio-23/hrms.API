﻿using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Enums;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using hrms.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Accounting.GetBackFromBreak
{
    public class GetBackFromBreakService : IGetBackFromBreakService
    {
        private readonly IRepository<TraceWorking> _traceWorkingRepositroy;
        private readonly IRepository<WorkingTraceReport> _workingTraceReportRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IRepository<EventNameTypeLookup> _eventTypeNameLookupRepository;
        private readonly IRepository<WorkingStatus> _workingStatusRepository;

        public GetBackFromBreakService(IRepository<TraceWorking> traceWorkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IGetCurrentUserIdService getCurrentUserIdService, IRepository<EventNameTypeLookup> eventTypeNameLookupRepository, IRepository<WorkingStatus> workingStatusRepository)
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
                var getAllTodaysTraces = await _traceWorkingRepositroy.Where(x => x.WorkingTraceId == getResult.Id)
                                                                      .OrderByDescending(x => x.EventOccurTime)
                                                                      .ToListAsync(cancellationToken);
                var getBreaStartStatus = await _eventTypeNameLookupRepository.FirstOrDefaultAsync(x => x.EventName == "Break" && x.EventType == "Start", cancellationToken) ?? throw new NotFoundException($"Break End type not found");
                var getBreakEndStatus = await _eventTypeNameLookupRepository.FirstOrDefaultAsync(x => x.EventName == "Break" && x.EventType == "End", cancellationToken) ?? throw new NotFoundException($"Break End type not found");

                var getCurrentStatus = await _workingStatusRepository.FirstOrDefaultAsync(x => x.Code == EnumDescription.GetDescription(CurrentStatusEnums.WORKING), cancellationToken) ?? throw new NotFoundException("Working status not found");

                var getLastTakenBreak = getAllTodaysTraces[0];

                if (getAllTodaysTraces.Count > 0 && getLastTakenBreak.EventNameTypeId != getBreaStartStatus.Id)
                {
                    throw new ArgumentException($"User {userId} is not on break for now");
                }
                if (getAllTodaysTraces.Count > 0 && getLastTakenBreak.EventNameTypeId == getBreakEndStatus.Id)
                {
                    throw new ArgumentException($"User {userId} already finished break");
                }

                var createNewTraceRecord = new TraceWorking()
                {
                    WorkingTraceId = getResult.Id,
                    EventNameTypeId = getBreakEndStatus.Id
                };

                var newTraceRecordAddResult = await _traceWorkingRepositroy.Add(createNewTraceRecord, cancellationToken);

                //todo - create to define max break up time and then calculate overdue minutes
                var breakTakeTime = getLastTakenBreak.EventOccurTime;
                var finishBreakTime = createNewTraceRecord.EventOccurTime;

                TimeSpan breakTimeDifference = finishBreakTime.Subtract(breakTakeTime);
                int differenceInMinutes = (int)breakTimeDifference.TotalMinutes;

                getResult.CurrentStatusId = getCurrentStatus.Id;
                getResult.UsedBreakMinutes = differenceInMinutes;
                await _workingTraceReportRepository.Update(getResult, cancellationToken);
                return ServiceResult<bool>.SuccessResult(true);
            }

            throw new Exception("Unexpected error occured on taking break");
        }
    }
}