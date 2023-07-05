using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class RefreshToken
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public virtual User? User { get; set; }
}
