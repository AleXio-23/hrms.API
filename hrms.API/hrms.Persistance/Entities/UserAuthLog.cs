using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UserAuthLog
{
    public long Id { get; set; }

    public int? UserId { get; set; }

    public DateTime Time { get; set; }

    public string? Ip { get; set; }

    public string? ActionType { get; set; }

    public string? ActionResult { get; set; }

    public string? ErrorReason { get; set; }

    public string? UserAgent { get; set; }

    public virtual User? User { get; set; }
}
