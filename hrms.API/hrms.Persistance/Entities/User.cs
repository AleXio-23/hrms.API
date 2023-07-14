using System;
using System.Collections.Generic;

namespace hrms.Persistance.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? PasswordExpires { get; set; }

    public DateTime? PasswordExpireDate { get; set; }

    public DateTime? PasswordChangeDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<UserJobPosition> UserJobPositions { get; set; } = new List<UserJobPosition>();

    public virtual UserProfile? UserProfile { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
