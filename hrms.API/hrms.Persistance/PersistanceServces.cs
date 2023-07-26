using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace hrms.Persistance
{
    public static class PersistanceServces
    {
        public static IServiceCollection RegisterPersistanceServces(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<UserProfile>, Repository<UserProfile>>();
            services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<UserRole>, Repository<UserRole>>();
            services.AddScoped<IRepository<Claim>, Repository<Claim>>();
            services.AddScoped<IRepository<UsersWorkSchedule>, Repository<UsersWorkSchedule>>();

            services.AddScoped<IRepository<Gender>, Repository<Gender>>();
            services.AddScoped<IRepository<Department>, Repository<Department>>();
            services.AddScoped<IRepository<JobPosition>, Repository<JobPosition>>();
            services.AddScoped<IRepository<UserJobPosition>, Repository<UserJobPosition>>();
            services.AddScoped<IRepository<VwUserSignInResponse>, Repository<VwUserSignInResponse>>();
            services.AddScoped<IRepository<WeekWorkingDay>, Repository<WeekWorkingDay>>();

            services.AddScoped<IRepository<UserAuthLog>, Repository<UserAuthLog>>();
            services.AddScoped<IRepository<TraceWorking>, Repository<TraceWorking>>();
            services.AddScoped<IRepository<WorkingTraceReport>, Repository<WorkingTraceReport>>();
            services.AddScoped<IRepository<EventNameTypeLookup>, Repository<EventNameTypeLookup>>();
            services.AddScoped<IRepository<WorkingStatus>, Repository<WorkingStatus>>();
            services.AddScoped<IRepository<WorkOnLateLog>, Repository<WorkOnLateLog>>();
            services.AddScoped<IRepository<LateFromBreak>, Repository<LateFromBreak>>();
            services.AddScoped<IRepository<NumberTypesConfiguration>, Repository<NumberTypesConfiguration>>();

            services.AddScoped<IRepository<CompanyHoliday>, Repository<CompanyHoliday>>();
            services.AddScoped<IRepository<HolidayRangeType>, Repository<HolidayRangeType>>();
            services.AddScoped<IRepository<HolidayType>, Repository<HolidayType>>();

            services.AddScoped<IRepository<DocumentType>, Repository<DocumentType>>();
            services.AddScoped<IRepository<UploadedDocument>, Repository<UploadedDocument>>();
            services.AddScoped<IRepository<UserUploadedDocument>, Repository<UserUploadedDocument>>();
            services.AddScoped<IRepository<PayedLeaf>, Repository<PayedLeaf>>();
            services.AddScoped<IRepository<QuartersConfiguration>, Repository<QuartersConfiguration>>();

            return services;
        }
    }
}
