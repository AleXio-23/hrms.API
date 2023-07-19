using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class TraceWorking
{
    public long Id { get; set; }

    public long WorkingTraceId { get; set; }

    public string EventName { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public DateTime EventOccurTime { get; set; }

    public string? Comment { get; set; }

    public bool? IsHonorable { get; set; }

    public virtual WorkingTraceReport WorkingTrace { get; set; } = null!;
}
