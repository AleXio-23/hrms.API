using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class HolidayRangeType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<HolidayType> HolidayTypes { get; set; } = new List<HolidayType>();
}
