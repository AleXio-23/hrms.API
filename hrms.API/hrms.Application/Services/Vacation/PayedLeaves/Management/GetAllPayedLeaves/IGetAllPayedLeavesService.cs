using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.PayedLeaves.Management.GetAllPayedLeaves
{
    public interface IGetAllPayedLeavesService
    {
        Task<ServiceResult<List<PayedLeaveDTOWithUserDTO>>> Execute(GetAllPayedLeavesServiceFilter filter, CancellationToken cancellationToken);
    }
}
