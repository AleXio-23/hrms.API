using hrms.Application.Services.Vacation.CheckAnyRegisteredHolidaysInRange;
using hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave;
using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.ApproveOrNotPayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.GetAllPayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.GetPayedLeave;
using hrms.Application.Services.Vacation.QuartersCounts;
using hrms.Application.Services.Vacation.UnpayedLeaves.AddOrUpdateUnpayedLeave;
using hrms.Application.Services.Vacation.UnpayedLeaves.GetCurrentActiveUnpayedLeaves;

namespace hrms.Application.Services.Vacation
{
    public interface IVacationsFacade
    {
        IAddOrUpdatePayedLeaveService AddOrUpdatePayedLeaveService { get; }
        IGetCurrentActivePayedLeavesService GetCurrentActivePayedLeavesService { get; }
        IQuartersCountsService QuartersCountsService { get; }

        ICheckAnyRegisteredHolidaysInRangeService CheckAnyRegisteredHolidaysInRangeService { get; }

        IGetCurrentActiveUnpayedLeavesService GetCurrentActiveUnpayedLeavesService { get; }
        IAddOrUpdateUnpayedLeaveService AddOrUpdateUnpayedLeaveService { get; }

        IGetAllPayedLeavesService GetAllPayedLeavesService { get; }
        IGetPayedLeaveService GetPayedLeaveService { get; }
        IApproveOrNotPayedLeavesService ApproveOrNotPayedLeavesService { get; }

    }
}
