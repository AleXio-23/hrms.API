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

    public virtual ICollection<UserAuthLog> UserAuthLogs { get; set; } = new List<UserAuthLog>();

    public virtual ICollection<UserJobPosition> UserJobPositions { get; set; } = new List<UserJobPosition>();

    public virtual UserProfile? UserProfile { get; set; }

    public virtual UserRole? UserRole { get; set; }

    public virtual ICollection<WorkingTraceReport> WorkingTraceReports { get; set; } = new List<WorkingTraceReport>();
}
