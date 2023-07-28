using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.AddOrUpdateSickLeave
{
    public class AddOrUpdateSickLeaveService : IAddOrUpdateSickLeaveService
    {
        public Task<ServiceResult<SickLeaveDTO>> Execute(SickLeaveDTO sickLeaveDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
