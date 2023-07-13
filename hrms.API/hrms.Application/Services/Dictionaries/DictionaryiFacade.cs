using hrms.Application.Services.Dictionaries.Departments.AddOrUpdateDepartment;
using hrms.Application.Services.Dictionaries.Departments.DeleteDepartment;
using hrms.Application.Services.Dictionaries.Departments.GetDepartment;
using hrms.Application.Services.Dictionaries.Departments.GetDepartments;
using hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender;
using hrms.Application.Services.Dictionaries.Gender.DeleteGender;
using hrms.Application.Services.Dictionaries.Gender.GetGender;
using hrms.Application.Services.Dictionaries.Gender.GetGenders;

namespace hrms.Application.Services.Dictionaries
{
    public class DictionaryiFacade : IDictionaryiFacade
    {
        public IGetGenderService GetGenderService { get; }
        public IAddOrUpdateGenderService AddOrUpdateGenderService { get; }
        public IGetGendersService GetGendersService { get; }

        public IDeleteGenerService DeleteGenerService { get; }

        public IAddOrUpdateDepartmentService AddOrUpdateDepartmentService { get; }

        public IDeleteDepartmentService DeleteDepartmentService { get; }

        public IGetDepartmentService GetDepartmentService { get; }

        public IGetDepartmentsService GetDepartmentsService { get; }

        public DictionaryiFacade(IGetGenderService getGenderService, IAddOrUpdateGenderService addOrUpdateGenderService, IGetGendersService getGendersService, IDeleteGenerService deleteGenerService, IAddOrUpdateDepartmentService addOrUpdateDepartmentService, IDeleteDepartmentService deleteDepartmentService, IGetDepartmentService getDepartmentService, IGetDepartmentsService getDepartmentsService)
        {
            GetGenderService = getGenderService;
            AddOrUpdateGenderService = addOrUpdateGenderService;
            GetGendersService = getGendersService;
            DeleteGenerService = deleteGenerService;
            AddOrUpdateDepartmentService = addOrUpdateDepartmentService;
            DeleteDepartmentService = deleteDepartmentService;
            GetDepartmentService = getDepartmentService;
            GetDepartmentsService = getDepartmentsService;
        }
    }
}
