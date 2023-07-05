using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class UserProfile
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? PhoneNumber1 { get; set; }

    public string? PhoneNumber2 { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? PersonalNumber { get; set; }

    public int? GenderId { get; set; }

    public DateTime RegisterDate { get; set; }

    public virtual Gender? Gender { get; set; }

    public virtual User? User { get; set; }
}
