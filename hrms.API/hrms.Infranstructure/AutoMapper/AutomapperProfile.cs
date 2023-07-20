using AutoMapper;
using hrms.Domain.Models.Accounting;
using hrms.Domain.Models.Configuration;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Dictionary.JobPositions;
using hrms.Domain.Models.User;
using hrms.Persistance.Entities;

namespace hrms.Infranstructure.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() : base()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<JobPosition, JobPositionDTO>().ReverseMap();
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            CreateMap<LateFromBreak , LateFromBreakDTO>().ReverseMap();
            CreateMap<WorkOnLateLog , WorkOnLateLogDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<NumberTypesConfiguration, NumberTypesConfigurationDTO>().ReverseMap();

            CreateMap<UserJobPosition, UserJobPositionDTO>()
                .ForMember(dep => dep.Department, opt => opt.MapFrom(x => x.Department))
                .ForMember(pos => pos.Position, opt => opt.MapFrom(x => x.Position))
                .ReverseMap();


        }
    }
}
