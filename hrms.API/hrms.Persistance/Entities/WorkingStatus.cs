using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class WorkingStatus
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Color { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<WorkingTraceReport> WorkingTraceReports { get; set; } = new List<WorkingTraceReport>();
}
