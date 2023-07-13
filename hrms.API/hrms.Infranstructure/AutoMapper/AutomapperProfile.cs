using AutoMapper;
using hrms.Domain.Models.Dictionary.Gender;

namespace hrms.Infranstructure.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() : base()
        {
            CreateMap<Persistance.Entities.Gender, GenderDTO>().ReverseMap();
        }
    }
}
