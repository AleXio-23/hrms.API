﻿using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Enums;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using hrms.Shared.Services;

namespace hrms.Application.Services.Accounting.StartAccounting
{
    public class StartAccountingService : IStartAccountingService
    {
        private readonly IRepository<TraceWorking> _traceWorkingRepositroy;
        private readonly IRepository<WorkingTraceReport> _workingTraceReportRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;
        private readonly IRepository<EventNameTypeLookup> _eventTypeNameLookupRepository;
        private readonly IRepository<WorkingStatus> _workingStatusRepository;

        public StartAccountingService(IRepository<TraceWorking> traceWorkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IGetCurrentUserIdService getCurrentUserIdService, IRepository<EventNameTypeLookup> eventTypeNameLookupRepository, IRepository<WorkingStatus> workingStatusRepository)
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
            //Check if work already started today and not finished yet
            var getResult = await _workingTraceReportRepository
                .FirstOrDefaultAsync(x => x.UserId == _getCurrentUserIdService.Execute()
                                        && x.WorkStarted.HasValue
                                        && x.WorkStarted.Value.Date == DateTime.Today.Date, cancellationToken);

            //Check if there is no record in report or if there is check, if work is finished
            //Then notifie that work already started 
            if (getResult != null && getResult.WorkEnded == null)
            {
                throw new ArgumentException($"User {userId} already started work today");
            }

            var getCurrentStauts = await _workingStatusRepository
                .FirstOrDefaultAsync(x => x.Code == EnumDescription.GetDescription(CurrentStatusEnums.WORKING), cancellationToken)
                ?? throw new NotFoundException("Error getting working start status");
            var getJobStartId = await _eventTypeNameLookupRepository.FirstOrDefaultAsync(x => x.EventName == "Job" && x.EventType == "Start", cancellationToken) ?? throw new NotFoundException($"Job Start type not found");

            //If job not started today
            if (getResult == null)
            {
                //first make record in report
                var newReport = new WorkingTraceReport()
                {
                    UserId = userId,
                    CurrentStatusId = getCurrentStauts?.Id
                };

                var newReportRecord = await _workingTraceReportRepository.Add(newReport, cancellationToken);

                var createNewTraceRecord = new TraceWorking()
                {
                    WorkingTraceId = newReportRecord.Id,
                    EventNameTypeId = getJobStartId.Id
                };

                var newTraceRecordAddResult = await _traceWorkingRepositroy.Add(createNewTraceRecord, cancellationToken);

                newReport.WorkStarted = newTraceRecordAddResult.EventOccurTime;
                await _workingTraceReportRepository.Update(newReport, cancellationToken);
            }

            //If job started, but was finished and renewed working
            else if (getResult != null && getResult.WorkEnded != null)
            {
                var createNewTraceRecord = new TraceWorking()
                {
                    WorkingTraceId = getResult.Id,
                    EventNameTypeId = getJobStartId.Id
                };
                var newTraceRecordAddResult = await _traceWorkingRepositroy.Add(createNewTraceRecord, cancellationToken);

                getResult.WorkStarted = newTraceRecordAddResult.EventOccurTime;
                getResult.WorkEnded = null;
                await _workingTraceReportRepository.Update(getResult, cancellationToken);
            }
            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}