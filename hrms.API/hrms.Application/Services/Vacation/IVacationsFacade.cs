using hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave;
using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;

namespace hrms.Application.Services.Vacation
{
    public interface IVacationsFacade
    {
        IAddOrUpdatePayedLeaveService AddOrUpdatePayedLeaveService { get; }
        IGetCurrentActivePayedLeavesService GetCurrentActivePayedLeavesService { get; }
    }
}
