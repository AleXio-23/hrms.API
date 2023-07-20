using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.GetBackFromBreak
{
    public interface IGetBackFromBreakService
    {
        Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken);
    }
}
