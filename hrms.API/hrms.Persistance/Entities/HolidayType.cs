using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class HolidayType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public int HolidayRangeTypeId { get; set; }

    public int CountUsageDaysPerRange { get; set; }

    public int MaxAmountReservedDaysForAnotherUsageRange { get; set; }

    public int? CanUseReservedDaysInAnotherRangeForDays { get; set; }

    public virtual HolidayRangeType HolidayRangeType { get; set; } = null!;
}
