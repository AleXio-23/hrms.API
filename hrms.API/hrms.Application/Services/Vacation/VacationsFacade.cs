using hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave;
using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;
using hrms.Application.Services.Vacation.QuartersCounts;

namespace hrms.Application.Services.Vacation
{
    public class VacationsFacade : IVacationsFacade
    {
        public VacationsFacade(IAddOrUpdatePayedLeaveService addOrUpdatePayedLeaveService, IGetCurrentActivePayedLeavesService getCurrentActivePayedLeavesService, IQuartersCountsService quartersCountsService)
        {
            AddOrUpdatePayedLeaveService = addOrUpdatePayedLeaveService;
            GetCurrentActivePayedLeavesService = getCurrentActivePayedLeavesService;
            QuartersCountsService = quartersCountsService;
        }

        public IAddOrUpdatePayedLeaveService AddOrUpdatePayedLeaveService { get; }

        public IGetCurrentActivePayedLeavesService GetCurrentActivePayedLeavesService { get; }

        public IQuartersCountsService QuartersCountsService { get; }
    }
}
