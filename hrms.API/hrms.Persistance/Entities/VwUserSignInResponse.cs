using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class VwUserSignInResponse
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? JobPositionId { get; set; }

    public string? JobPositionName { get; set; }

    public int? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }
}
