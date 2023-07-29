using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetUnpayedLeave
{
    public interface IGetUnpayedLeaveService
    {
        Task<ServiceResult<UnpayedLeaveDTOWithUserDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
