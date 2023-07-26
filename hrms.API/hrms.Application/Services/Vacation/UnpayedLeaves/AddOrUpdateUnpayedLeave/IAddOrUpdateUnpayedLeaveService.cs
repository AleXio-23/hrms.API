using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.AddOrUpdateUnpayedLeave
{
    public interface IAddOrUpdateUnpayedLeaveService
    {
        Task<ServiceResult<UnpayedLeaveDTO>> Execute(UnpayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken);
    }
}
