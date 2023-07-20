using hrms.Domain.Models.Accounting;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.LogLateFromBreak.AddLogLateFromBreak
{
    public interface IAddLogLateFromBreakService
    {
        Task<ServiceResult<LateFromBreakDTO>> Execute(LateFromBreakDTO lateFromBreakDTO, CancellationToken cancellationToken);
    }
}
