using hrms.Domain.Models.Accounting;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.LogLateFromBreak.GetLogsLateFromBreak
{
    public interface IGetLogsLateFromBreakService
    {
        Task<ServiceResult<List<LateFromBreakDTO>>> Execute(LateFromBreakFilter filter, CancellationToken cancellationToken);
    }
}
