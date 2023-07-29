using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetAllUnpayedLeaves
{
    public interface IGetAllUnpayedLeavesService
    {
        Task<ServiceResult<List<UnpayedLeaveDTOWithUserDTO>>> Execute(GetAllUnpayedLeavesServiceFilter filter, CancellationToken cancellationToken);
    }
}
