using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class DayOff
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public bool? Approved { get; set; }

    public int? ApprovedByUserId { get; set; }

    public string? Comment { get; set; }

    public virtual User? ApprovedByUser { get; set; }

    public virtual User User { get; set; } = null!;
}
