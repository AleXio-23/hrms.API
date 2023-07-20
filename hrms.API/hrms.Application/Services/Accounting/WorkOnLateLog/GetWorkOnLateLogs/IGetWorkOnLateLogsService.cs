using hrms.Domain.Models.Accounting;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.WorkOnLateLog.GetWorkOnLateLogs
{
    public interface IGetWorkOnLateLogsService
    {
        Task<ServiceResult<List<WorkOnLateLogDTO>>> Execute(WorkOnLateLogFilter filter, CancellationToken cancellationToken);
    }
}
