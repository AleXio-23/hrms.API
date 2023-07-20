namespace hrms.Domain.Models.Accounting
{
    public class LateFromBreakFilter
    {
        public int? UserId { get; set; }

        public long? WorkingTraceReportId { get; set; }

        public long? TraceWorking { get; set; }

        public int? LateMinutes { get; set; }

        public string? Comment { get; set; }

        public DateTime? LogStartDate { get; set; }
        public DateTime? LogEndDate { get; set; }

        public bool? IsHonorable { get; set; }
    }
}
