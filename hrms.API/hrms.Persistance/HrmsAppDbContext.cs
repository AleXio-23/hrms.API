﻿using System;
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

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserJobPosition> UserJobPositions { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<VwUserSignInResponse> VwUserSignInResponses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HRMS;Persist Security Info=True;User Id=;Password=;Trusted_Connection=true;TrustServerCertificate=true;");

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

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__RoleI__2F10007B"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__UserI__2E1BDC42"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD2A41FBEA");
                        j.ToTable("UserRoles", "ums");
                    });
        });

        modelBuilder.Entity<UserJobPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserJobP__3214EC0751E7C351");

            entity.ToTable("UserJobPositions", "ums");

            entity.HasIndex(e => new { e.UserId, e.PositionId, e.DepartmentId }, "UQ__UserJobP__7D31727119EA65F2").IsUnique();

            entity.HasOne(d => d.Department).WithMany(p => p.UserJobPositions)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__UserJobPo__Depar__6477ECF3");

            entity.HasOne(d => d.Position).WithMany(p => p.UserJobPositions)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__UserJobPo__Posit__6383C8BA");

            entity.HasOne(d => d.User).WithMany(p => p.UserJobPositions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserJobPo__UserI__628FA481");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
