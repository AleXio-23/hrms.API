using hrms.Application.Services.Accounting.StartAccounting;

namespace hrms.Application.Services.Accounting
{
    public class AccountingFacade : IAccountingFacade
    {
        public AccountingFacade(IStartAccountingService startAccountingService)
        {
            StartAccountingService = startAccountingService;
        }

        public IStartAccountingService StartAccountingService { get; }
    }
}
