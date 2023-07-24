namespace hrms.Domain.Models.Dictionary.Vacations
{
    public class HolidayTypeDTO
    {
        public int? Id { get; set; }

        public string? Code { get; set; } = null!;

        public string? Name { get; set; }

        public int? HolidayRangeTypeId { get; set; }

        public int? CountUsageDaysPerRange { get; set; }

        public int? MaxAmountReservedDaysForAnotherUsageRange { get; set; }
    }
}
