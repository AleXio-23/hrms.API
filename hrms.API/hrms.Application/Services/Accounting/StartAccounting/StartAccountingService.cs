using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.StartAccounting
{
    public class StartAccountingService : IStartAccountingService
    {
        private readonly IRepository<TraceWorking> _traceWirkingRepositroy;
        private readonly IRepository<WorkingTraceReport> _workingTraceReportRepository;
        private readonly IRepository<Persistance.Entities.User> _userRepository;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;

        public StartAccountingService(IRepository<TraceWorking> traceWirkingRepositroy, IRepository<WorkingTraceReport> workingTraceReportRepository, IRepository<Persistance.Entities.User> userRepository, IGetCurrentUserIdService getCurrentUserIdService)
        {
            _traceWirkingRepositroy = traceWirkingRepositroy;
            _workingTraceReportRepository = workingTraceReportRepository;
            _userRepository = userRepository;
            _getCurrentUserIdService = getCurrentUserIdService;
        }

        public async Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken)
        {
            //first make record in report
            var newReport = new WorkingTraceReport()
            {
                UserId = _getCurrentUserIdService.Execute(),

            };

            var newReportRecord = await _workingTraceReportRepository.Add(newReport, cancellationToken);
            var t = newReportRecord.WorkStarted;

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
