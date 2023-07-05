using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class Claim
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? SortIndex { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
