using hrms.Application.Services.Vacation.CheckAnyRegisteredHolidaysInRange;
using hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave;
using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.ApproveOrNotPayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.GetAllPayedLeaves;
using hrms.Application.Services.Vacation.PayedLeaves.Management.GetPayedLeave;
using hrms.Application.Services.Vacation.QuartersCounts;
using hrms.Application.Services.Vacation.SickLeaves.AddOrUpdateSickLeave;
using hrms.Application.Services.Vacation.SickLeaves.GetCurrentActiveSickLeaves;
using hrms.Application.Services.Vacation.SickLeaves.Management.ApproveOrNotSickLeaves;
using hrms.Application.Services.Vacation.SickLeaves.Management.GetAllSickLeaves;
using hrms.Application.Services.Vacation.SickLeaves.Management.GetSickLeave;
using hrms.Application.Services.Vacation.UnpayedLeaves.AddOrUpdateUnpayedLeave;
using hrms.Application.Services.Vacation.UnpayedLeaves.GetCurrentActiveUnpayedLeaves;
using hrms.Application.Services.Vacation.UnpayedLeaves.Management.ApproveOrNotUnpayedLeaves;
using hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetAllUnpayedLeaves;
using hrms.Application.Services.Vacation.UnpayedLeaves.Management.GetUnpayedLeave;

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

        IApproveOrNotUnpayedLeavesService ApproveOrNotUnpayedLeavesService { get; }
        IGetAllUnpayedLeavesService GetAllUnpayedLeavesService { get; }
        IGetUnpayedLeaveService GetUnpayedLeaveService { get; }

        IAddOrUpdateSickLeaveService AddOrUpdateSickLeaveService { get; }
        IGetCurrentActiveSickLeavesService GetCurrentActiveSickLeavesService { get; }
        IApproveOrNotSickLeavesService ApproveOrNotSickLeavesService { get; }
        IGetAllSickLeavesService GetAllSickLeavesService { get; }
        IGetSickLeaveService GetSickLeaveService { get; }

    }
}
