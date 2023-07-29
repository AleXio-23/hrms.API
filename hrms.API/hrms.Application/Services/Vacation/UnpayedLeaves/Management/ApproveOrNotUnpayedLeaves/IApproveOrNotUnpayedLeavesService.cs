using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.Management.ApproveOrNotUnpayedLeaves
{
    public interface IApproveOrNotUnpayedLeavesService
    {
        Task<ServiceResult<UnpayedLeaveDTOWithUserDTO>> Execute(ApproveOrNotLeavesRequest request, CancellationToken cancellationToken);
    }
}
