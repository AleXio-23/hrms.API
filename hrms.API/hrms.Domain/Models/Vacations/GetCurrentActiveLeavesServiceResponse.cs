namespace hrms.Domain.Models.Vacations
{
    public class GetCurrentActiveLeavesServiceResponse
    {
        /// <summary>
        /// Returns value for user payed leaves days for quarter/Year 
        /// </summary>
        public int? TotalUserLeaveDays { get; set; }

        /// <summary>
        /// Returns value for user left leaves days for quarter/Year 
        /// </summary>
        public int? LeftLeaveDays { get; set; }

        /// <summary>
        /// Return available days (if its configured for current request) from previous quarter/Year
        /// </summary>
        public int? RemainingAvailableDaysFromPastQuarterOrYear { get; set; }
    }
}
