using hrms.Domain.Models.Accounting;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLog
{
    public interface IGetWorkOnLateLogService
    {
        Task<ServiceResult<WorkOnLateLogDTO>> Execute(int logId, CancellationToken cancellationToken);
    }
}
