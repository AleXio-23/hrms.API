using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.StartAccounting;
using hrms.Application.Services.Accounting.TakeBreak;

namespace hrms.Application.Services.Accounting
{
    public class AccountingFacade : IAccountingFacade
    {
        public AccountingFacade(IStartAccountingService startAccountingService, IFinishAccountingService finishAccountingService, ITakeBreakService takeBreakService)
        {
            StartAccountingService = startAccountingService;
            FinishAccountingService = finishAccountingService;
            TakeBreakService = takeBreakService;
        }

        public IStartAccountingService StartAccountingService { get; }

        public IFinishAccountingService FinishAccountingService { get; }

        public ITakeBreakService TakeBreakService { get; }
    }
}
