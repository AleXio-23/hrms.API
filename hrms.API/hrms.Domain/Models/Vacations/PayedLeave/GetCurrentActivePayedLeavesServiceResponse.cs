namespace hrms.Domain.Models.Vacations.PayedLeave
{
    public class GetCurrentActivePayedLeavesServiceResponse
    {
        //var totalUsedDays = userPayedLeaves.Sum(x => x.CountDays);
        //var leftPayedLeavesDays = holidayType.CountUsageDaysPerRange - totalUsedDays;
        //remainingAvailableDaysFromPastQuarterOrYear

        /// <summary>
        /// Returns value for user payed leaves days for quarter/Year 
        /// </summary>
        public int? TotalUserPayedLeavesDays { get; set; }

        /// <summary>
        /// Returns value for user left leaves days for quarter/Year 
        /// </summary>
        public int? LeftPayedLeavesDays { get; set; }

        /// <summary>
        /// Return available days (if its configured for current request) from previous quarter/Year
        /// </summary>
        public int? RemainingAvailableDaysFromPastQuarterOrYear { get; set; }
    }
}
