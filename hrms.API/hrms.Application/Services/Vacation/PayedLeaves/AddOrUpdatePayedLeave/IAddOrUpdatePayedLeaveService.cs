using hrms.Domain.Models.Vacations.PayedLeave;

namespace hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave
{
    public interface IAddOrUpdatePayedLeaveService
    {
        Task<PayedLeaveDTO> Execute(PayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken);
    }
}
