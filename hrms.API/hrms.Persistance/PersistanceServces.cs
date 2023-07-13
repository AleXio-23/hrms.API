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
            services.AddScoped<IRepository<Gender>, Repository<Gender>>();
            services.AddScoped<IRepository<Department>, Repository<Department>>();
            services.AddScoped<IRepository<JobPosition>, Repository<JobPosition>>();
            services.AddScoped<IRepository<JobPositionDepartment>, Repository<JobPositionDepartment>>();
            services.AddScoped<IRepository<VwUserSignInResponse>, Repository<VwUserSignInResponse>>();

            return services;
        }
    }
}
