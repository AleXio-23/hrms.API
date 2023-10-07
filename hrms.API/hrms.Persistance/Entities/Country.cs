using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class Country
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public bool HasStates { get; set; }

    public int SortIndex { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
