﻿using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class LateFromBreak
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public long WorkingTraceReportId { get; set; }

    public long TraceWorkingId { get; set; }

    public int LateMinutes { get; set; }

    public string? Comment { get; set; }

    public DateTime LogDate { get; set; }

    public bool? IsHonorable { get; set; }

    public virtual TraceWorking TraceWorking { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual WorkingTraceReport WorkingTraceReport { get; set; } = null!;
}
