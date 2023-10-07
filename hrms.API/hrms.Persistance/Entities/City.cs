using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class City
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int SortIndex { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country? Country { get; set; }

    public virtual State? State { get; set; }
}
