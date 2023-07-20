using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.GetBackFromBreak;
using hrms.Application.Services.Accounting.StartAccounting;
using hrms.Application.Services.Accounting.TakeBreak;
using hrms.Application.Services.Accounting.WorkOnLateLog.AddWorkOnLateLog;
using hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLogs;

namespace hrms.Application.Services.Accounting
{
    public interface IAccountingFacade
    {
        IStartAccountingService StartAccountingService { get; }
        IFinishAccountingService FinishAccountingService { get; }
        ITakeBreakService TakeBreakService { get; }
        IGetBackFromBreakService GetBackFromBreakService { get; }
        IAddWorkOnLateLogService AddWorkOnLateLogService { get; }
        IGetWorkOnLateLogsService GetWorkOnLateLogsService { get; }
    }
}
