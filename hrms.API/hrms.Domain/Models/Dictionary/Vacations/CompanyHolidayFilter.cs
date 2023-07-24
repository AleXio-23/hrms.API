namespace hrms.Domain.Models.Dictionary.Vacations
{
    public class CompanyHolidayFilter
    {
        public DateTime? EventDateStart { get; set; }
        public DateTime? EventDateEnd { get; set; }
        public string? EventDescription { get; set; }
        public int? NotifyBeforeDays { get; set; }
        public int? NotifyBeforeHours { get; set; }
        public bool? IsActive { get; set; }
    }
}
