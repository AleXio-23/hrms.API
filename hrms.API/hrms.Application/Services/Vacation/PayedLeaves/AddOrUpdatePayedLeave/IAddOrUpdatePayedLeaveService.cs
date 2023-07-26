using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave
{
    public interface IAddOrUpdatePayedLeaveService
    {
        Task<ServiceResult<PayedLeaveDTO>> Execute(PayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken);
    }
}
