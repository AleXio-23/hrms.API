using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.GetCurrentActiveUnpayedLeaves
{
    public interface IGetCurrentActiveUnpayedLeavesService
    {
        Task<ServiceResult<GetCurrentActiveLeavesServiceResponse>> Execute(int userId, CancellationToken cancellationToken);
    }
}
