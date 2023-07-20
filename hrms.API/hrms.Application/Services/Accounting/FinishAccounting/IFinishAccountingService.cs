using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.FinishAccounting
{
    public interface IFinishAccountingService
    {
        Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken);
    }
}
