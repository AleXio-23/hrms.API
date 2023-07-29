using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.SickLeaves.Management.GetSickLeave
{
    public interface IGetSickLeaveService
    {
        Task<ServiceResult<SickLeaveDTOWithUserDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
