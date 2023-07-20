using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class EventNameTypeLookup
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public string EventType { get; set; } = null!;

    public virtual ICollection<TraceWorking> TraceWorkings { get; set; } = new List<TraceWorking>();
}
