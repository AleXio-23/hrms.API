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

    public virtual ICollection<DayOff> DayOffApprovedByUsers { get; set; } = new List<DayOff>();

    public virtual ICollection<DayOff> DayOffUsers { get; set; } = new List<DayOff>();

    public virtual ICollection<LateFromBreak> LateFromBreaks { get; set; } = new List<LateFromBreak>();

    public virtual ICollection<PayedLeaf> PayedLeafApprovedByUsers { get; set; } = new List<PayedLeaf>();

    public virtual ICollection<PayedLeaf> PayedLeafUsers { get; set; } = new List<PayedLeaf>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<SickLeaf> SickLeafApprovedByUsers { get; set; } = new List<SickLeaf>();

    public virtual ICollection<SickLeaf> SickLeafUsers { get; set; } = new List<SickLeaf>();

    public virtual ICollection<UnpayedLeaf> UnpayedLeafApprovedByUsers { get; set; } = new List<UnpayedLeaf>();

    public virtual ICollection<UnpayedLeaf> UnpayedLeafUsers { get; set; } = new List<UnpayedLeaf>();

    public virtual ICollection<UserAuthLog> UserAuthLogs { get; set; } = new List<UserAuthLog>();

    public virtual ICollection<UserJobPosition> UserJobPositions { get; set; } = new List<UserJobPosition>();

    public virtual UserLocation? UserLocation { get; set; }

    public virtual UserProfile? UserProfile { get; set; }

    public virtual UserRole? UserRole { get; set; }

    public virtual ICollection<UserUploadedDocument> UserUploadedDocuments { get; set; } = new List<UserUploadedDocument>();

    public virtual ICollection<WorkOnLateLog> WorkOnLateLogs { get; set; } = new List<WorkOnLateLog>();

    public virtual ICollection<WorkingTraceReport> WorkingTraceReports { get; set; } = new List<WorkingTraceReport>();
}
