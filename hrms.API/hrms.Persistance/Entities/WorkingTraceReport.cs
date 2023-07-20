using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class WorkingTraceReport
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public DateTime? WorkStarted { get; set; }

    public DateTime? WorkEnded { get; set; }

    public int UsedBreakMinutes { get; set; }

    public int LateOnWorkMinutes { get; set; }

    public int BreakTimeOverdueMinutes { get; set; }

    public int OvertimeWorkingMinutes { get; set; }

    public virtual ICollection<TraceWorking> TraceWorkings { get; set; } = new List<TraceWorking>();

    public virtual User User { get; set; } = null!;
}
