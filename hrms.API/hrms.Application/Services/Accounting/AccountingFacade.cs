using hrms.Application.Services.Accounting.FinishAccounting;
using hrms.Application.Services.Accounting.StartAccounting;

namespace hrms.Application.Services.Accounting
{
    public class AccountingFacade : IAccountingFacade
    {
        public AccountingFacade(IStartAccountingService startAccountingService, IFinishAccountingService finishAccountingService)
        {
            StartAccountingService = startAccountingService;
            FinishAccountingService = finishAccountingService;
        }

        public IStartAccountingService StartAccountingService { get; }

        public IFinishAccountingService FinishAccountingService { get; }
    }
}
