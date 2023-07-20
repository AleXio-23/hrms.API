using hrms.Application.Services.Accounting.WorkOnLateLog.AddWorkOnLateLog;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration;
using hrms.Domain.Models.Accounting;
using hrms.Infranstructure.Services.CurrentUserId;
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
        private readonly IAddWorkOnLateLogService _workOnLateLogService;
        private readonly IGetNumberTypesConfigurationService _getNumberTypesConfigurationService;

        public StartAccountingService(IRepository<TraceWorking> traceWorkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IGetCurrentUserIdService getCurrentUserIdService, IRepository<EventNameTypeLookup> eventTypeNameLookupRepository, IRepository<WorkingStatus> workingStatusRepository, IAddWorkOnLateLogService workOnLateLogService, IGetNumberTypesConfigurationService getNumberTypesConfigurationService)
        {
            _traceWorkingRepositroy = traceWorkingRepositroy;
            _workingTraceReportRepository = workingTraceReportRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
            _eventTypeNameLookupRepository = eventTypeNameLookupRepository;
            _workingStatusRepository = workingStatusRepository;
            _workOnLateLogService = workOnLateLogService;
            _getNumberTypesConfigurationService = getNumberTypesConfigurationService;
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

                //Check if work start is late => then log it
                var getConfigurationForStartingHour = await _getNumberTypesConfigurationService.Execute("WorkTimeStartHour", cancellationToken).ConfigureAwait(false);
                var getConfigurationForStartingMinute = await _getNumberTypesConfigurationService.Execute("WorkTimeStartMinute", cancellationToken).ConfigureAwait(false);

                if (getConfigurationForStartingHour == null || getConfigurationForStartingHour.Data == null)
                {
                    throw new NotFoundException("WorkTimeStartHour not found in configuration");
                }
                if (getConfigurationForStartingMinute == null || getConfigurationForStartingMinute.Data == null)
                {
                    throw new NotFoundException("WorkTimeStartMinute not found in configuration");
                }

                var datetimeNow = DateTime.Now;
                DateTime generateTodayworkingStartTime = new(datetimeNow.Year, datetimeNow.Month, datetimeNow.Day, getConfigurationForStartingHour.Data.Value, getConfigurationForStartingMinute.Data.Value, 0);
                DateTime workStartedDateTime = newReport.WorkStarted ?? throw new Exception("Work started date is null");

                TimeSpan timeDifference = workStartedDateTime - generateTodayworkingStartTime;
                int minutesDifference = (int)timeDifference.TotalMinutes;

                if (minutesDifference > 0)
                {
                    var createNewWorkOnLateLog = new WorkOnLateLogDTO()
                    {
                        UserId = userId,
                        WorkingTraceReportId = newReport.Id,
                        LateMinutes = minutesDifference
                    };

                    await _workOnLateLogService.Execute(createNewWorkOnLateLog, cancellationToken);
                }
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
