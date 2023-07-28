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
    public class VacationsFacade : IVacationsFacade
    {
        public VacationsFacade(IAddOrUpdatePayedLeaveService addOrUpdatePayedLeaveService, IGetCurrentActivePayedLeavesService getCurrentActivePayedLeavesService, IQuartersCountsService quartersCountsService, ICheckAnyRegisteredHolidaysInRangeService checkAnyRegisteredHolidaysInRangeService, IGetCurrentActiveUnpayedLeavesService getCurrentActiveUnpayedLeavesService, IAddOrUpdateUnpayedLeaveService addOrUpdateUnpayedLeaveService, IGetAllPayedLeavesService getAllPayedLeavesService, IGetPayedLeaveService getPayedLeaveService, IApproveOrNotPayedLeavesService approveOrNotPayedLeavesService)
        {
            AddOrUpdatePayedLeaveService = addOrUpdatePayedLeaveService;
            GetCurrentActivePayedLeavesService = getCurrentActivePayedLeavesService;
            QuartersCountsService = quartersCountsService;
            CheckAnyRegisteredHolidaysInRangeService = checkAnyRegisteredHolidaysInRangeService;
            GetCurrentActiveUnpayedLeavesService = getCurrentActiveUnpayedLeavesService;
            AddOrUpdateUnpayedLeaveService = addOrUpdateUnpayedLeaveService;
            GetAllPayedLeavesService = getAllPayedLeavesService;
            GetPayedLeaveService = getPayedLeaveService;
            ApproveOrNotPayedLeavesService = approveOrNotPayedLeavesService;
        }

        public IAddOrUpdatePayedLeaveService AddOrUpdatePayedLeaveService { get; }

        public IGetCurrentActivePayedLeavesService GetCurrentActivePayedLeavesService { get; }

        public IQuartersCountsService QuartersCountsService { get; }

        public ICheckAnyRegisteredHolidaysInRangeService CheckAnyRegisteredHolidaysInRangeService { get; }

        public IGetCurrentActiveUnpayedLeavesService GetCurrentActiveUnpayedLeavesService { get; }

        public IAddOrUpdateUnpayedLeaveService AddOrUpdateUnpayedLeaveService { get; }

        public IGetAllPayedLeavesService GetAllPayedLeavesService { get; }

        public IGetPayedLeaveService GetPayedLeaveService { get; }

        public IApproveOrNotPayedLeavesService ApproveOrNotPayedLeavesService { get; }
    }
}
