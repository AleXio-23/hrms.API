using hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender;
using hrms.Application.Services.Dictionaries.Gender.DeleteGender;
using hrms.Application.Services.Dictionaries.Gender.GetGender;
using hrms.Application.Services.Dictionaries.Gender.GetGenders;

namespace hrms.Application.Services.Dictionaries
{
    public interface IDictionaryiFacade
    {
        IGetGenderService GetGenderService { get; }
        IAddOrUpdateGenderService AddOrUpdateGenderService { get; }
        IGetGendersService GetGendersService { get; }
        IDeleteGenerService DeleteGenerService { get; }
    }
}
