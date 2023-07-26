using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves
{
    public interface IGetCurrentActivePayedLeavesService
    {
        Task<ServiceResult<GetCurrentActivePayedLeavesServiceResponse>> Execute(int userId, CancellationToken cancellationToken);
    }
}
