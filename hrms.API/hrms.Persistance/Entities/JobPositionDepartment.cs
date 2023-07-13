using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class JobPositionDepartment
{
    public int? PositionId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual JobPosition? Position { get; set; }
}
