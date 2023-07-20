namespace hrms.Domain.Models.Accounting
{
    public class WorkOnLateLogDTO
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public long WorkingTraceReportId { get; set; }
        public int LateMinutes { get; set; }

        public string? Comment { get; set; }

        public bool? IsHonorable { get; set; }
        public DateTime LogDate { get; set; }
    }
}
