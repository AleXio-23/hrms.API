using hrms.Domain.Models.Vacations;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.Management.ApproveOrNotSickLeaves
{
    public interface IApproveOrNotSickLeavesService
    {
        Task<ServiceResult<SickLeaveDTOWithUserDTO>> Execute(ApproveOrNotLeavesRequest request, CancellationToken cancellationToken);
    }
}
