using hrms.Domain.Models.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.GetCurrentActiveSickLeaves
{
    public class GetCurrentActiveSickLeavesService : IGetCurrentActiveSickLeavesService
    {
        public Task<ServiceResult<GetCurrentActiveLeavesServiceResponse>> Execute(int userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
