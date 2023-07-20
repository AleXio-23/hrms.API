using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.GetBackFromBreak;
using hrms.Application.Services.Accounting.StartAccounting;
using hrms.Application.Services.Accounting.TakeBreak;

namespace hrms.Application.Services.Accounting
{
    public class AccountingFacade : IAccountingFacade
    {
        public AccountingFacade(IStartAccountingService startAccountingService, IFinishAccountingService finishAccountingService, ITakeBreakService takeBreakService, IGetBackFromBreakService getBackFromBreakService)
        {
            StartAccountingService = startAccountingService;
            FinishAccountingService = finishAccountingService;
            TakeBreakService = takeBreakService;
            GetBackFromBreakService = getBackFromBreakService;
        }

        public IStartAccountingService StartAccountingService { get; }

        public IFinishAccountingService FinishAccountingService { get; }

        public ITakeBreakService TakeBreakService { get; }

        public IGetBackFromBreakService GetBackFromBreakService { get; }
    }
}
