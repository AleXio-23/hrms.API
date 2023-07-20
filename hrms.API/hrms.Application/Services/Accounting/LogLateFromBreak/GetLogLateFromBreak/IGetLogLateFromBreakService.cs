using hrms.Domain.Models.Accounting;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.LogLateFromBreak.GetLogLateFromBreak
{
    public interface IGetLogLateFromBreakService
    {
        Task<ServiceResult<LateFromBreakDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
