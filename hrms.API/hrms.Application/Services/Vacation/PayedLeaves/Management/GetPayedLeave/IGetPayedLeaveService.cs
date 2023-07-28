using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.PayedLeaves.Management.GetPayedLeave
{
    public interface IGetPayedLeaveService
    {
        Task<ServiceResult<PayedLeaveDTOWithUserDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
