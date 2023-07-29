using hrms.Domain.Models.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.GetCurrentActiveSickLeaves
{
    public interface IGetCurrentActiveSickLeavesService
    {
        Task<ServiceResult<GetCurrentActiveLeavesServiceResponse>> Execute(int userId, CancellationToken cancellationToken);
    }
}
