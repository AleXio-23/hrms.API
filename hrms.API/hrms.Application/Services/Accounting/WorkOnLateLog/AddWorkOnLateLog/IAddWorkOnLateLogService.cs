using hrms.Domain.Models.Accounting;
using hrms.Shared.Models;

namespace hrms.Application.Services.Accounting.WorkOnLateLog.AddWorkOnLateLog
{
    public interface IAddWorkOnLateLogService
    {
        Task<ServiceResult<WorkOnLateLogDTO>> Execute(WorkOnLateLogDTO workOnLateLogDTO, CancellationToken cancellationToken);
    }
}
