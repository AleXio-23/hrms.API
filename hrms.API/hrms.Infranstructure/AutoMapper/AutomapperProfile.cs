using AutoMapper;
using hrms.Domain.Models.Dictionary.Departments;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Dictionary.JobPositions;
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
        }
    }
}
