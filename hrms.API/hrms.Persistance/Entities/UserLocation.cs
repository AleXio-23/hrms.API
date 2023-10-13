using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UserLocation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public string? Address { get; set; }

    public virtual City? City { get; set; }

    public virtual Country? Country { get; set; }

    public virtual State? State { get; set; }

    public virtual User User { get; set; } = null!;
}
