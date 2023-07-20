using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using hrms.Persistance.Entities;

namespace hrms.Persistance;

public partial class HrmsAppDbContext : DbContext
{
    public HrmsAppDbContext()
    {
    }

    public HrmsAppDbContext(DbContextOptions<HrmsAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EventNameTypeLookup> EventNameTypeLookups { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<LateFromBreak> LateFromBreaks { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<NumberTypesConfiguration> NumberTypesConfigurations { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TraceWorking> TraceWorkings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuthLog> UserAuthLogs { get; set; }

    public virtual DbSet<UserJobPosition> UserJobPositions { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<VwUserSignInResponse> VwUserSignInResponses { get; set; }

    public virtual DbSet<WorkOnLateLog> WorkOnLateLogs { get; set; }

    public virtual DbSet<WorkingStatus> WorkingStatuses { get; set; }

    public virtual DbSet<WorkingTraceReport> WorkingTraceReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Claims__3214EC075A5542B3");

            entity.ToTable("Claims", "ums");

            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC0787DF6BE1");

            entity.ToTable("Departments", "dictionary");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<EventNameTypeLookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventNam__3214EC07D3BA5CD5");

            entity.ToTable("EventNameTypeLookup", "hrms");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EventType)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Gender__3214EC07951B00B9");

            entity.ToTable("Gender", "dictionary");

            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Value)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobPosit__3214EC07BA459616");

            entity.ToTable("JobPositions", "dictionary");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<LateFromBreak>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LateFrom__3214EC0727768743");

            entity.ToTable("LateFromBreak");

            entity.Property(e => e.Comment).HasMaxLength(4000);

            entity.HasOne(d => d.TraceWorkingNavigation).WithMany(p => p.LateFromBreaks)
                .HasForeignKey(d => d.TraceWorking)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LateFromB__Trace__1F63A897");

            entity.HasOne(d => d.User).WithMany(p => p.LateFromBreaks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LateFromB__UserI__1D7B6025");

            entity.HasOne(d => d.WorkingTraceReport).WithMany(p => p.LateFromBreaks)
                .HasForeignKey(d => d.WorkingTraceReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LateFromB__Worki__1E6F845E");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logs__3214EC07065BEAD2");

            entity.ToTable("Logs", "trace");

            entity.Property(e => e.LogDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LogLevel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MethodName).HasMaxLength(1000);
        });

        modelBuilder.Entity<NumberTypesConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NumberTy__3214EC07E3D394A0");

            entity.ToTable("NumberTypesConfigurations", "config");

            entity.Property(e => e.ConfigName)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC0742E1BF6C");

            entity.ToTable("RefreshTokens", "ums");

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__37A5467C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07FB91C759");

            entity.ToTable("Roles", "ums");

            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.Claims).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleClaim",
                    r => r.HasOne<Claim>().WithMany()
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleClaim__Claim__34C8D9D1"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleClaim__RoleI__33D4B598"),
                    j =>
                    {
                        j.HasKey("RoleId", "ClaimId").HasName("PK__RoleClai__24082F236BAA3EAD");
                        j.ToTable("RoleClaims", "ums");
                    });
        });

        modelBuilder.Entity<TraceWorking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TraceWor__3214EC07F94EFD5F");

            entity.ToTable("TraceWorking", "hrms");

            entity.Property(e => e.EventOccurTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.EventNameType).WithMany(p => p.TraceWorkings)
                .HasForeignKey(d => d.EventNameTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TraceWork__Event__15DA3E5D");

            entity.HasOne(d => d.WorkingTrace).WithMany(p => p.TraceWorkings)
                .HasForeignKey(d => d.WorkingTraceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TraceWork__Worki__14E61A24");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07EF3ED36C");

            entity.ToTable("Users", "ums");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342D4BEC49").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PasswordChangeDate).HasDefaultValueSql("(dateadd(month,(2),getdate()))");
            entity.Property(e => e.PasswordExpireDate).HasDefaultValueSql("(dateadd(month,(2),getdate()))");
            entity.Property(e => e.PasswordExpires)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAuthLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAuth__3214EC07F689DE51");

            entity.ToTable("UserAuthLogs", "report");

            entity.Property(e => e.ActionResult).HasMaxLength(255);
            entity.Property(e => e.ActionType).HasMaxLength(255);
            entity.Property(e => e.ErrorReason).HasMaxLength(1024);
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("IP");
            entity.Property(e => e.Time).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UserAgent).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.UserAuthLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserAuthL__UserI__4D5F7D71");
        });

        modelBuilder.Entity<UserJobPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserJobP__3214EC07006CED34");

            entity.ToTable("UserJobPositions", "ums");

            entity.HasIndex(e => new { e.UserId, e.PositionId, e.DepartmentId }, "UQ__UserJobP__7D317271A0CD5621").IsUnique();

            entity.HasOne(d => d.Department).WithMany(p => p.UserJobPositions)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__UserJobPo__Depar__6FE99F9F");

            entity.HasOne(d => d.Position).WithMany(p => p.UserJobPositions)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__UserJobPo__Posit__6EF57B66");

            entity.HasOne(d => d.User).WithMany(p => p.UserJobPositions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserJobPo__UserI__6E01572D");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProf__3214EC07927A3856");

            entity.ToTable("UserProfile", "ums");

            entity.HasIndex(e => e.UserId, "UQ__UserProf__1788CC4DAFCD4CC0").IsUnique();

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.Firstname).HasMaxLength(100);
            entity.Property(e => e.Lastname).HasMaxLength(100);
            entity.Property(e => e.PersonalNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Gender).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__UserProfi__Gende__3F466844");

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.UserId)
                .HasConstraintName("FK__UserProfi__UserI__3E52440B");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK__UserRole__AF2760ADD9FB1796");

            entity.ToTable("UserRoles", "ums");

            entity.HasIndex(e => e.UserId, "UQ__UserRole__1788CC4D10F4D071").IsUnique();

            entity.HasIndex(e => e.RoleId, "UQ__UserRole__8AFACE1B337A00A6").IsUnique();

            entity.HasOne(d => d.Role).WithOne(p => p.UserRole)
                .HasForeignKey<UserRole>(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__RoleI__75A278F5");

            entity.HasOne(d => d.User).WithOne(p => p.UserRole)
                .HasForeignKey<UserRole>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRoles__UserI__74AE54BC");
        });

        modelBuilder.Entity<VwUserSignInResponse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_UserSignInResponse");

            entity.Property(e => e.DepartmentName).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.JobPositionName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<WorkOnLateLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkOnLa__3214EC078570ED78");

            entity.ToTable("WorkOnLateLogs", "hrms");

            entity.Property(e => e.Comment).HasMaxLength(4000);

            entity.HasOne(d => d.User).WithMany(p => p.WorkOnLateLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkOnLat__UserI__19AACF41");

            entity.HasOne(d => d.WorkingTraceReport).WithMany(p => p.WorkOnLateLogs)
                .HasForeignKey(d => d.WorkingTraceReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkOnLat__Worki__1A9EF37A");
        });

        modelBuilder.Entity<WorkingStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkingS__3214EC07BEED21BD");

            entity.ToTable("WorkingStatuses", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<WorkingTraceReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkingT__3214EC0744E78264");

            entity.ToTable("WorkingTraceReport", "report");

            entity.HasIndex(e => e.WorkStarted, "index_WorkStarted_datetime");

            entity.HasOne(d => d.CurrentStatus).WithMany(p => p.WorkingTraceReports)
                .HasForeignKey(d => d.CurrentStatusId)
                .HasConstraintName("FK__WorkingTr__Curre__03BB8E22");

            entity.HasOne(d => d.User).WithMany(p => p.WorkingTraceReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkingTr__UserI__7EF6D905");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
