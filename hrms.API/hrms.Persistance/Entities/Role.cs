using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual UserRole? UserRole { get; set; }

    public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
}
