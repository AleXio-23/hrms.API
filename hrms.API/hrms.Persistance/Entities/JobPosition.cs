using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class JobPosition
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }
}
