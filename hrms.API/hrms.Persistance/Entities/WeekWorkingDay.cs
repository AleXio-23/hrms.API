using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class WeekWorkingDay
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<UsersWorkSchedule> UsersWorkSchedules { get; set; } = new List<UsersWorkSchedule>();
}
