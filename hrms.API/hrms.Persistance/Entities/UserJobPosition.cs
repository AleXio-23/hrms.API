using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UserJobPosition
{
    public int? UserId { get; set; }

    public int? PositionId { get; set; }

    public virtual JobPosition? Position { get; set; }

    public virtual User? User { get; set; }
}
