using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.StartAccounting
{
    public interface IStartAccountingService
    {
        Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken);
    }
}
