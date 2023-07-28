using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.Management.GetAllSickLeaves
{
    public class GetAllSickLeavesService : IGetAllSickLeavesService
    {
        public Task<ServiceResult<List<SickLeaveDTOWithUserDTO>>> Execute(GetAllSickLeavesServiceFilter filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
