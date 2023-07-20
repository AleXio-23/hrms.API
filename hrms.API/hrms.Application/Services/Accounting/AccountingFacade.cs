using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.GetBackFromBreak;
using hrms.Application.Services.Accounting.LogLateFromBreak.AddLogLateFromBreak;
using hrms.Application.Services.Accounting.LogLateFromBreak.GetLogLateFromBreak;
using hrms.Application.Services.Accounting.LogLateFromBreak.GetLogsLateFromBreak;
using hrms.Application.Services.Accounting.StartAccounting;
using hrms.Application.Services.Accounting.TakeBreak;
using hrms.Application.Services.Accounting.WorkOnLateLog.AddWorkOnLateLog;
using hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLogs;

namespace hrms.Application.Services.Accounting
{
    public class AccountingFacade : IAccountingFacade
    {
        public AccountingFacade(IStartAccountingService startAccountingService, IFinishAccountingService finishAccountingService, ITakeBreakService takeBreakService, IGetBackFromBreakService getBackFromBreakService, IAddWorkOnLateLogService addWorkOnLateLogService, IGetWorkOnLateLogsService getWorkOnLateLogsService, IAddLogLateFromBreakService addLogLateFromBreakService, IGetLogLateFromBreakService getLogLateFromBreakService, IGetLogsLateFromBreakService getLogsLateFromBreakService)
        {
            StartAccountingService = startAccountingService;
            FinishAccountingService = finishAccountingService;
            TakeBreakService = takeBreakService;
            GetBackFromBreakService = getBackFromBreakService;
            AddWorkOnLateLogService = addWorkOnLateLogService;
            GetWorkOnLateLogsService = getWorkOnLateLogsService;
            AddLogLateFromBreakService = addLogLateFromBreakService;
            GetLogLateFromBreakService = getLogLateFromBreakService;
            GetLogsLateFromBreakService = getLogsLateFromBreakService;
        }

        public IStartAccountingService StartAccountingService { get; }

        public IFinishAccountingService FinishAccountingService { get; }

        public ITakeBreakService TakeBreakService { get; }

        public IGetBackFromBreakService GetBackFromBreakService { get; }

        public IAddWorkOnLateLogService AddWorkOnLateLogService { get; }

        public IGetWorkOnLateLogsService GetWorkOnLateLogsService { get; }

        public IAddLogLateFromBreakService AddLogLateFromBreakService { get; }

        public IGetLogLateFromBreakService GetLogLateFromBreakService { get; }

        public IGetLogsLateFromBreakService GetLogsLateFromBreakService { get; }
    }
}
