using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class State
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int SortIndex { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country? Country { get; set; }

    public virtual ICollection<UserLocation> UserLocations { get; set; } = new List<UserLocation>();
}
