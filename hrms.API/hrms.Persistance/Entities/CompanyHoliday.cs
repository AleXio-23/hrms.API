using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class CompanyHoliday
{
    public int Id { get; set; }

    public DateTime EventDate { get; set; }

    public string? EventDescription { get; set; }

    public int? NotifyBeforeDays { get; set; }

    public int? NotifyBeforeHours { get; set; }

    public bool? IsActive { get; set; }
}
