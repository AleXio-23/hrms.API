using hrms.Application.Services.User.GetUser;
using hrms.Application.Services.Vacation.QuartersCounts;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves
{
    public class GetCurrentActivePayedLeavesService : IGetCurrentActivePayedLeavesService
    {
        private readonly IRepository<PayedLeaf> _payedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IRepository<QuartersConfiguration> _quarterCfgRepository;
        private readonly IGetUserService _getUserService;
        private readonly IQuartersCountsService _quartersCountsService;

        public GetCurrentActivePayedLeavesService(IRepository<PayedLeaf> payedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IRepository<QuartersConfiguration> quarterCfgRepository, IGetUserService getUserService, IQuartersCountsService quartersCountsService)
        {
            _payedLeaveRepository = payedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _quarterCfgRepository = quarterCfgRepository;
            _getUserService = getUserService;
            _quartersCountsService = quartersCountsService;
        }

        public async Task<string> Execute(int userId, CancellationToken cancellationToken)
        {
            //get payed leave holiday with its range type
            var holidayTypeCode = "payed_leave";
            var holidayType = await _holidayTypeRepository
                .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == holidayTypeCode)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (holidayType == null || holidayType.HolidayRangeType == null)
            {
                throw new NotFoundException($"Holiday type or range type for payed leave not found");
            }
            //Check holidays range type, if its Per quarter or per year
            bool? isQuarterRange =
              holidayType?.HolidayRangeType.Code == "per_quarter" || (holidayType?.HolidayRangeType.Code == "per_year"
                      ? false : throw new NotFoundException("Holday range type not found"));

            //Get start and end dates for if its quarter or year type range 
            var currentYear = DateTime.Now.Year;
            var getCurrentQuarter = await _quartersCountsService.GetCurrentQuarter(cancellationToken).ConfigureAwait(false);

            //Calculate quarter start/end dates  (for use if its per quarter type range)
            var currentquarterStartDate = new DateTime(currentYear, getCurrentQuarter.QuarterStartsMonth, getCurrentQuarter.QuarterStartsDay, 0, 0, 0, millisecond: 0);
            var currentquarterEndDate = new DateTime(currentYear, getCurrentQuarter.QuarterEndsMonth, getCurrentQuarter.QuarterEndsDay, 0, 0, 0, millisecond: 0);

            //calculate holday calculate start / end dates depends on if its perquarter or per year status
            var startDate = isQuarterRange == true ? currentquarterStartDate : new DateTime(currentYear, 1, 1);
            var endDate = isQuarterRange == true ? currentquarterEndDate : new DateTime(currentYear, 12, 31);

            //Get all payed leaves during previously calculated ranges
            var userPayedLeaves = await _payedLeaveRepository
             .Where(x => x.UserId == userId && x.DateStart >= startDate && x.DateEnd <= endDate)
             .ToListAsync(cancellationToken).ConfigureAwait(false);

            //Count total used days and left days for current Year/Quarter
            var totalUsedDays = userPayedLeaves.Sum(x => x.CountDays);
            var leftPayedLeavesDays = holidayType.CountUsageDaysPerRange - totalUsedDays;


            var user = await _getUserService.Execute(userId, cancellationToken).ConfigureAwait(false);

            int? remainingAvailableDaysFromPastQuarterOrYear = null;

            if (holidayType.MaxAmountReservedDaysForAnotherUsageRange > 0)
            {
                if (isQuarterRange == true)
                {
                    //Get last quarter date range

                }
                else if (isQuarterRange == false)
                {
                    //Last year
                }
            }

            return null;
        }

    }
}
