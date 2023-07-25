using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class QuartersConfiguration
{
    public int Id { get; set; }

    public string CodeName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime QuarterStarts { get; set; }

    public DateTime QuarterEnds { get; set; }
}
