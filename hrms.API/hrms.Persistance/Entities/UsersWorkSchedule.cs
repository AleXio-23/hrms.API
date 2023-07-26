using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UsersWorkSchedule
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int WeekWorkingDayId { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public virtual WeekWorkingDay WeekWorkingDay { get; set; } = null!;
}
