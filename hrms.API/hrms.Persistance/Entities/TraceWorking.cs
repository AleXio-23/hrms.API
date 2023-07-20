namespace hrms.Persistance.Entities;

public partial class TraceWorking
{
    public long Id { get; set; }

    public long WorkingTraceId { get; set; }

    public int EventNameTypeId { get; set; }

    public DateTime EventOccurTime { get; set; }

    public string? Comment { get; set; }

    public bool? IsHonorable { get; set; }

    public virtual EventNameTypeLookup EventNameType { get; set; } = null!;

    public virtual WorkingTraceReport WorkingTrace { get; set; } = null!;
}
