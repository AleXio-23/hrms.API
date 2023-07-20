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

    public int? CurrentStatusId { get; set; }

    public virtual WorkingStatus? CurrentStatus { get; set; }

    public virtual ICollection<LateFromBreak> LateFromBreaks { get; set; } = new List<LateFromBreak>();

    public virtual ICollection<TraceWorking> TraceWorkings { get; set; } = new List<TraceWorking>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WorkOnLateLog> WorkOnLateLogs { get; set; } = new List<WorkOnLateLog>();
}
