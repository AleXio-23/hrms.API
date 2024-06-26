﻿using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
