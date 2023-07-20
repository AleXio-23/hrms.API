namespace hrms.Domain.Models.Accounting
{
    public class WorkOnLateLogFilter
    {
        public int? UserId { get; set; }

        public long? WorkingTraceReportId { get; set; }

        public string? Comment { get; set; }

        public bool? IsHonorable { get; set; }
        public DateTime? LogDateStart { get; set; }
        public DateTime? LogDateEnd { get; set; }
    }
}
