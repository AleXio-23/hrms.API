using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.PayedLeaves.Management.ApproveOrNotPayedLeaves
{
    public interface IApproveOrNotPayedLeavesService
    {
        Task<ServiceResult<PayedLeaveDTOWithUserDTO>> Execute(ApproveOrNotLeavesRequest request, CancellationToken cancellationToken);
    }
}
