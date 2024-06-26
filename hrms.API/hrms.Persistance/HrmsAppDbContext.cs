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

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<CompanyHoliday> CompanyHolidays { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DayOff> DayOffs { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }

    public virtual DbSet<EventNameTypeLookup> EventNameTypeLookups { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<HolidayRangeType> HolidayRangeTypes { get; set; }

    public virtual DbSet<HolidayType> HolidayTypes { get; set; }

    public virtual DbSet<JobPosition> JobPositions { get; set; }

    public virtual DbSet<LateFromBreak> LateFromBreaks { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<NumberTypesConfiguration> NumberTypesConfigurations { get; set; }

    public virtual DbSet<PayedLeaf> PayedLeaves { get; set; }

    public virtual DbSet<QuartersConfiguration> QuartersConfigurations { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SickLeaf> SickLeaves { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<TraceWorking> TraceWorkings { get; set; }

    public virtual DbSet<UnpayedLeaf> UnpayedLeaves { get; set; }

    public virtual DbSet<UploadedDocument> UploadedDocuments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuthLog> UserAuthLogs { get; set; }

    public virtual DbSet<UserJobPosition> UserJobPositions { get; set; }

    public virtual DbSet<UserLocation> UserLocations { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserUploadedDocument> UserUploadedDocuments { get; set; }

    public virtual DbSet<UsersWorkSchedule> UsersWorkSchedules { get; set; }

    public virtual DbSet<VwUserSignInResponse> VwUserSignInResponses { get; set; }

    public virtual DbSet<WeekWorkingDay> WeekWorkingDays { get; set; }

    public virtual DbSet<WorkOnLateLog> WorkOnLateLogs { get; set; }

    public virtual DbSet<WorkingStatus> WorkingStatuses { get; set; }

    public virtual DbSet<WorkingTraceReport> WorkingTraceReports { get; set; }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3214EC075953D372");

            entity.ToTable("City", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SortIndex).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__City__CountryId__6991A7CB");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__City__StateId__6A85CC04");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Claims__3214EC075A5542B3");

            entity.ToTable("Claims", "ums");

            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<CompanyHoliday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompanyH__3214EC07E7AABDB7");

            entity.ToTable("CompanyHolidays", "dictionary");

            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC07DBA6E08A");

            entity.ToTable("Country", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SortIndex).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<DayOff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DayOffs__3214EC077551D492");

            entity.ToTable("DayOffs", "vacation");

            entity.Property(e => e.Comment).HasMaxLength(1024);

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.DayOffApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK__DayOffs__Approve__7814D14C");

            entity.HasOne(d => d.User).WithMany(p => p.DayOffUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DayOffs__UserId__762C88DA");
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

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC079C280121");

            entity.ToTable("DocumentTypes", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsDocumentSizeLimited)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.MaxDocumentSizeInMbsToUpload).HasDefaultValueSql("((100))");
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

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(1000);
        });

        modelBuilder.Entity<HolidayRangeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HolidayR__3214EC079C4C7915");

            entity.ToTable("HolidayRangeTypes", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<HolidayType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HolidayT__3214EC0752EEC8FD");

            entity.ToTable("HolidayTypes", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.HolidayRangeType).WithMany(p => p.HolidayTypes)
                .HasForeignKey(d => d.HolidayRangeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HolidayTy__Holid__27C3E46E");
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
            entity.HasKey(e => e.Id).HasName("PK__LateFrom__3214EC072BB4AA3F");

            entity.ToTable("LateFromBreak", "hrms");

            entity.Property(e => e.Comment).HasMaxLength(4000);
            entity.Property(e => e.LogDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TraceWorking).WithMany(p => p.LateFromBreaks)
                .HasForeignKey(d => d.TraceWorkingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LateFromB__Trace__467D75B8");

            entity.HasOne(d => d.User).WithMany(p => p.LateFromBreaks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LateFromB__UserI__44952D46");

            entity.HasOne(d => d.WorkingTraceReport).WithMany(p => p.LateFromBreaks)
                .HasForeignKey(d => d.WorkingTraceReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LateFromB__Worki__4589517F");
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

        modelBuilder.Entity<PayedLeaf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PayedLea__3214EC072C84CDD5");

            entity.ToTable("PayedLeaves", "vacation");

            entity.Property(e => e.Comment).HasMaxLength(1024);

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.PayedLeafApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK__PayedLeav__Appro__6E8B6712");

            entity.HasOne(d => d.User).WithMany(p => p.PayedLeafUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PayedLeav__UserI__6BAEFA67");
        });

        modelBuilder.Entity<QuartersConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quarters__3214EC07028ACBE8");

            entity.ToTable("QuartersConfiguration", "config");

            entity.Property(e => e.CodeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(255);
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

        modelBuilder.Entity<SickLeaf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SickLeav__3214EC076DEBB036");

            entity.ToTable("SickLeaves", "vacation");

            entity.Property(e => e.Comment).HasMaxLength(1024);

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.SickLeafApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK__SickLeave__Appro__4EDDB18F");

            entity.HasOne(d => d.User).WithMany(p => p.SickLeafUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SickLeave__UserI__4CF5691D");

            entity.HasMany(d => d.Documents).WithMany(p => p.SickLeaves)
                .UsingEntity<Dictionary<string, object>>(
                    "SickLeaveDocumnt",
                    r => r.HasOne<UserUploadedDocument>().WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SickLeave__Docum__52AE4273"),
                    l => l.HasOne<SickLeaf>().WithMany()
                        .HasForeignKey("SickLeaveId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__SickLeave__SickL__51BA1E3A"),
                    j =>
                    {
                        j.HasKey("SickLeaveId", "DocumentId").HasName("PK__SickLeav__222C828DA594FAD4");
                        j.ToTable("SickLeaveDocumnts", "vacation");
                    });
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__State__3214EC071AFD4BF0");

            entity.ToTable("State", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SortIndex).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__State__CountryId__5F141958");
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

        modelBuilder.Entity<UnpayedLeaf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UnpayedL__3214EC079E7BFB04");

            entity.ToTable("UnpayedLeaves", "vacation");

            entity.Property(e => e.Comment).HasMaxLength(1024);

            entity.HasOne(d => d.ApprovedByUser).WithMany(p => p.UnpayedLeafApprovedByUsers)
                .HasForeignKey(d => d.ApprovedByUserId)
                .HasConstraintName("FK__UnpayedLe__Appro__73501C2F");

            entity.HasOne(d => d.User).WithMany(p => p.UnpayedLeafUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UnpayedLe__UserI__7167D3BD");
        });

        modelBuilder.Entity<UploadedDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Uploaded__3214EC07C59E2675");

            entity.ToTable("UploadedDocuments", "documents");

            entity.Property(e => e.DocumentName).HasMaxLength(1024);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UploadDate).HasDefaultValueSql("(getdate())");
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

        modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLoca__3214EC07C7FF8B24");

            entity.ToTable("UserLocation", "ums");

            entity.HasIndex(e => e.UserId, "UQ__UserLoca__1788CC4DA2653CBA").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(1024);

            entity.HasOne(d => d.City).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__UserLocat__CityI__04459E07");

            entity.HasOne(d => d.Country).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__UserLocat__Count__025D5595");

            entity.HasOne(d => d.State).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__UserLocat__State__035179CE");

            entity.HasOne(d => d.User).WithOne(p => p.UserLocation)
                .HasForeignKey<UserLocation>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserLocat__UserI__0169315C");
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
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC07E46E6FDD");

            entity.ToTable("UserRoles", "ums");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRoles__RoleI__1293BD5E");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__119F9925");
        });

        modelBuilder.Entity<UserUploadedDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserUplo__3214EC073B6A2710");

            entity.ToTable("UserUploadedDocuments", "documents");

            entity.HasIndex(e => e.DocumentTypeId, "IX_UserUploadedDocuments_DocumentTypeId");

            entity.HasIndex(e => e.UploadedByUserId, "IX_UserUploadedDocuments_UploadedByUser");

            entity.Property(e => e.DocumentTypeIfNotFoundInDicitonary).HasMaxLength(1024);
            entity.Property(e => e.UploadDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Document).WithMany(p => p.UserUploadedDocuments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserUploa__Docum__12C8C788");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.UserUploadedDocuments)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK__UserUploa__Docum__11D4A34F");

            entity.HasOne(d => d.UploadedByUser).WithMany(p => p.UserUploadedDocuments)
                .HasForeignKey(d => d.UploadedByUserId)
                .HasConstraintName("FK__UserUploa__Uploa__0FEC5ADD");
        });

        modelBuilder.Entity<UsersWorkSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersWor__3214EC070E288B2B");

            entity.ToTable("UsersWorkSchedule", "ums");

            entity.HasOne(d => d.User).WithMany(p => p.UsersWorkSchedules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_WOrkSc");

            entity.HasOne(d => d.WeekWorkingDay).WithMany(p => p.UsersWorkSchedules)
                .HasForeignKey(d => d.WeekWorkingDayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersWork__WeekW__324172E1");
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

        modelBuilder.Entity<WeekWorkingDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WeekWork__3214EC075AC183F7");

            entity.ToTable("WeekWorkingDay", "dictionary");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkOnLateLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkOnLa__3214EC075DC8A8BA");

            entity.ToTable("WorkOnLateLogs", "hrms");

            entity.Property(e => e.Comment).HasMaxLength(4000);
            entity.Property(e => e.LogDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.WorkOnLateLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkOnLat__UserI__382F5661");

            entity.HasOne(d => d.WorkingTraceReport).WithMany(p => p.WorkOnLateLogs)
                .HasForeignKey(d => d.WorkingTraceReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkOnLat__Worki__3B0BC30C");
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
