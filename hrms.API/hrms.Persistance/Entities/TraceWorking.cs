using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class TraceWorking
{
    public long Id { get; set; }

    public long WorkingTraceId { get; set; }

    public int EventNameTypeId { get; set; }

    public DateTime EventOccurTime { get; set; }

    public virtual EventNameTypeLookup EventNameType { get; set; } = null!;

    public virtual ICollection<LateFromBreak> LateFromBreaks { get; set; } = new List<LateFromBreak>();

    public virtual WorkingTraceReport WorkingTrace { get; set; } = null!;
}
