using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.TakeBreak
{
    public interface ITakeBreakService
    {
        Task<ServiceResult<bool>> Execute(CancellationToken cancellationToken);
    }
}
