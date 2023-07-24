namespace hrms.Domain.Models.Dictionary.Vacations
{
    public class CompanyHolidayDTO
    {
        public int? Id { get; set; }

        public DateTime? EventDate { get; set; }

        public string? EventDescription { get; set; }

        public int? NotifyBeforeDays { get; set; }

        public int? NotifyBeforeHours { get; set; }

        public bool? IsActive { get; set; }
    }
}
