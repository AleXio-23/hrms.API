using hrms.Application.Services.User.GetUser;
using hrms.Application.Services.Vacation.QuartersCounts;
using hrms.Domain.Models.Vacations;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.GetCurrentActiveUnpayedLeaves
{
    public class GetCurrentActiveUnpayedLeavesService : IGetCurrentActiveUnpayedLeavesService
    {
        private readonly IRepository<UnpayedLeaf> _unPayedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IGetUserService _getUserService;
        private readonly IQuartersCountsService _quartersCountsService;

        public GetCurrentActiveUnpayedLeavesService(IRepository<UnpayedLeaf> unPayedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IGetUserService getUserService, IQuartersCountsService quartersCountsService)
        {
            _unPayedLeaveRepository = unPayedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _getUserService = getUserService;
            _quartersCountsService = quartersCountsService;
        }



        /// <summary>
        /// Check for specific user, how much unpayed leaves left for now (quarter/year type rage)
        /// Calculate how much days left from previous quarter/year
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ServiceResult<GetCurrentActiveLeavesServiceResponse>> Execute(int userId, CancellationToken cancellationToken)
        {
            //get unpayed leave holiday with its range type
            var holidayTypeCode = "unpayed_leave";
            var holidayType = await _holidayTypeRepository
                .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == holidayTypeCode)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (holidayType == null || holidayType.HolidayRangeType == null)
            {
                throw new NotFoundException($"Holiday type or range type for unpayed leave not found");
            }
            //Check holidays range type, if its Per quarter or per year
            bool? isQuarterRange =
              holidayType?.HolidayRangeType.Code == "per_quarter" || (holidayType?.HolidayRangeType.Code == "per_year"
                      ? false : throw new NotFoundException("Holday range type not found"));

            //Get start and end dates for if its quarter or year type range 
            int currentYear = DateTime.Now.Year;
            var getCurrentQuarter = await _quartersCountsService.GetCurrentQuarter(cancellationToken).ConfigureAwait(false);

            //Calculate quarter start/end dates  (for use if its per quarter type range)
            var currentquarterStartDate = new DateTime(currentYear, getCurrentQuarter.QuarterStartsMonth, getCurrentQuarter.QuarterStartsDay, 0, 0, 0, millisecond: 0);
            var currentquarterEndDate = new DateTime(currentYear, getCurrentQuarter.QuarterEndsMonth, getCurrentQuarter.QuarterEndsDay, 0, 0, 0, millisecond: 0);

            //calculate holday calculate start / end dates depends on if its perquarter or per year status
            var startDate = isQuarterRange == true ? currentquarterStartDate : new DateTime(currentYear, 1, 1);
            var endDate = isQuarterRange == true ? currentquarterEndDate : new DateTime(currentYear, 12, 31);

            //Get all unpayed leaves during previously calculated ranges
            var userunPayedLeaves = await _unPayedLeaveRepository
             .Where(x => x.UserId == userId && x.DateStart >= startDate && x.DateEnd <= endDate && x.Approved != false)
             .ToListAsync(cancellationToken).ConfigureAwait(false);

            //Get user for whom calculating remaining AvailableDaysFromPastQuarterOrYear
            var user = await _getUserService.Execute(userId, cancellationToken).ConfigureAwait(false);
            if (user == null || user.Data == null || user.Data.UserProfileDTO == null)
            {
                throw new ArgumentException("Error getting user information");
            }

            int? remainingAvailableDaysFromPastQuarterOrYear = null;

            if (holidayType.MaxAmountReservedDaysForAnotherUsageRange > 0 && holidayType.CanUseReservedDaysInAnotherRangeForDays != null && holidayType.CanUseReservedDaysInAnotherRangeForDays > 0)
            {
                if (isQuarterRange == true)
                {
                    //Get last quarter date range
                    var (quartersConfiguration, PreviousQuarterStart, previousQuarterEnd) = await _quartersCountsService.GetPreviousQuarterWithDateRanges(cancellationToken).ConfigureAwait(false);

                    //Check if current datetime is in range of CanUseReservedDaysInAnotherRangeForDays
                    var availableforDays = startDate.AddDays(holidayType.CanUseReservedDaysInAnotherRangeForDays ?? 0);
                    if (DateTime.Now <= availableforDays)
                    {
                        //If user registered before the previous quarter, than user can use avalable days from past quarter if its configured to have
                        if (user.Data?.UserProfileDTO.RegisterDate <= previousQuarterEnd)
                        {
                            //Get all used unpayed leave for previous quarter
                            var userUnpayedLeavesInPreviousQuarter = await _unPayedLeaveRepository
                                        .Where(x => x.UserId == userId && x.DateStart >= PreviousQuarterStart && x.DateEnd <= previousQuarterEnd && x.Approved != false)
                                        .ToListAsync(cancellationToken).ConfigureAwait(false);

                            //Count total used days
                            var totalUsedDaysForPreviusQuarter = userUnpayedLeavesInPreviousQuarter.Sum(x => x.CountDays);
                            //Count left days
                            var leftUnpayedLeavesDaysForPreviusQuarter = holidayType.CountUsageDaysPerRange - totalUsedDaysForPreviusQuarter < 0 ? 0 : holidayType.CountUsageDaysPerRange - totalUsedDaysForPreviusQuarter;
                            //Calculate remaining max available days
                            remainingAvailableDaysFromPastQuarterOrYear = leftUnpayedLeavesDaysForPreviusQuarter > holidayType.MaxAmountReservedDaysForAnotherUsageRange
                                ? holidayType.MaxAmountReservedDaysForAnotherUsageRange
                                : leftUnpayedLeavesDaysForPreviusQuarter;
                        }
                    }
                }
                else if (isQuarterRange == false)
                {
                    //Last year
                    var startDateForPreviusyear = new DateTime(currentYear - 1, 1, 1);
                    var endDateForPreviusyear = new DateTime(currentYear - 1, 12, 31);

                    //Check if current datetime is in range of CanUseReservedDaysInAnotherRangeForDays
                    var availableforDays = startDate.AddDays(holidayType.CanUseReservedDaysInAnotherRangeForDays ?? 0);
                    if (DateTime.Now <= availableforDays)
                    {
                        if (user.Data?.UserProfileDTO.RegisterDate <= endDateForPreviusyear)
                        {
                            //Get all used unpayed leave for previous year
                            var userUnpayedLeavesInPreviousYear = await _unPayedLeaveRepository
                                        .Where(x => x.UserId == userId && x.DateStart >= startDateForPreviusyear && x.DateEnd <= endDateForPreviusyear && x.Approved != false)
                                        .ToListAsync(cancellationToken).ConfigureAwait(false);

                            //Count total used days
                            var totalUsedDaysForPreviusYear = userUnpayedLeavesInPreviousYear.Sum(x => x.CountDays);
                            //Count left days
                            var leftUnpayedLeavesDaysForPreviusYear = holidayType.CountUsageDaysPerRange - totalUsedDaysForPreviusYear < 0 ? 0 : holidayType.CountUsageDaysPerRange - totalUsedDaysForPreviusYear;
                            //Calculate remaining max available days
                            remainingAvailableDaysFromPastQuarterOrYear = leftUnpayedLeavesDaysForPreviusYear > holidayType.MaxAmountReservedDaysForAnotherUsageRange
                                ? holidayType.MaxAmountReservedDaysForAnotherUsageRange
                                : leftUnpayedLeavesDaysForPreviusYear;
                        }
                    }
                }
            }

            //Count, if person had remainingAvailableDaysFromPastQuarterOrYear, than reduce it
            if (remainingAvailableDaysFromPastQuarterOrYear != null && remainingAvailableDaysFromPastQuarterOrYear > 0)
            {
                var totalUsedDays = userunPayedLeaves.Sum(x => x.CountDays);
                remainingAvailableDaysFromPastQuarterOrYear = totalUsedDays > remainingAvailableDaysFromPastQuarterOrYear ? 0 : remainingAvailableDaysFromPastQuarterOrYear - totalUsedDays;
                var left = totalUsedDays > remainingAvailableDaysFromPastQuarterOrYear ? totalUsedDays - remainingAvailableDaysFromPastQuarterOrYear : 0;
                var leftUnpayedLeavesDays = holidayType.CountUsageDaysPerRange - left;

                var response = new GetCurrentActiveLeavesServiceResponse()
                {
                    TotalUserLeaveDays = totalUsedDays,
                    LeftLeaveDays = leftUnpayedLeavesDays,
                    RemainingAvailableDaysFromPastQuarterOrYear = remainingAvailableDaysFromPastQuarterOrYear
                };

                return ServiceResult<GetCurrentActiveLeavesServiceResponse>.SuccessResult(response);
            }
            else
            {
                var totalUsedDays = userunPayedLeaves.Sum(x => x.CountDays);
                var leftUnpayedLeavesDays = holidayType.CountUsageDaysPerRange - totalUsedDays;
                var response = new GetCurrentActiveLeavesServiceResponse()
                {
                    TotalUserLeaveDays = totalUsedDays,
                    LeftLeaveDays = leftUnpayedLeavesDays,
                    RemainingAvailableDaysFromPastQuarterOrYear = remainingAvailableDaysFromPastQuarterOrYear
                };

                return ServiceResult<GetCurrentActiveLeavesServiceResponse>.SuccessResult(response);
            }
        }
    }
}
