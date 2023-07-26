using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class QuartersConfiguration
{
    public int Id { get; set; }

    public string CodeName { get; set; } = null!;

    public string? Description { get; set; }

    public int QuarterStartsMonth { get; set; }

    public int QuarterStartsDay { get; set; }

    public int QuarterEndsMonth { get; set; }

    public int QuarterEndsDay { get; set; }
}
