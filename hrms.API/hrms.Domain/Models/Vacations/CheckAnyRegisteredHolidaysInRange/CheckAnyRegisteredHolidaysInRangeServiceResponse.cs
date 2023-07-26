namespace hrms.Domain.Models.Vacations.CheckAnyRegisteredHolidaysInRange
{
    public class CheckAnyRegisteredHolidaysInRangeServiceResponse
    {
        public bool HasOverlap { get; set; }
        public string? HolidayType { get; set; }
        public DateTime? HolidayStart { get; set; }
        public DateTime? HolidayEnd { get; set; }
    }
}
