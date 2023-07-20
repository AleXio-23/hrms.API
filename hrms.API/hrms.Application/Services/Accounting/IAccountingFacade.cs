using hrms.Application.Services.Accounting.StartAccounting;

namespace hrms.Application.Services.Accounting
{
    public interface IAccountingFacade
    {
        IStartAccountingService StartAccountingService { get; }
    }
}
