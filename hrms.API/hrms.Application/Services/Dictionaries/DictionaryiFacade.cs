using hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender;
using hrms.Application.Services.Dictionaries.Gender.DeleteGender;
using hrms.Application.Services.Dictionaries.Gender.GetGender;
using hrms.Application.Services.Dictionaries.Gender.GetGenders;

namespace hrms.Application.Services.Dictionaries
{
    public class DictionaryiFacade : IDictionaryiFacade
    {
        public DictionaryiFacade(IGetGenderService getGenderService, IAddOrUpdateGenderService addOrUpdateGenderService, IGetGendersService getGendersService, IDeleteGenerService deleteGenerService)
        {
            GetGenderService = getGenderService;
            AddOrUpdateGenderService = addOrUpdateGenderService;
            GetGendersService = getGendersService;
            DeleteGenerService = deleteGenerService;
        }

        public IGetGenderService GetGenderService { get; }
        public IAddOrUpdateGenderService AddOrUpdateGenderService { get; }
        public IGetGendersService GetGendersService { get; }

        public IDeleteGenerService DeleteGenerService { get; }
    }
}
