using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.AddOrUpdateSickLeave
{
    public interface IAddOrUpdateSickLeaveService
    {
        Task<ServiceResult<SickLeaveDTO>> Execute(SickLeaveDTO sickLeaveDTO, CancellationToken cancellationToken);
    }
}
