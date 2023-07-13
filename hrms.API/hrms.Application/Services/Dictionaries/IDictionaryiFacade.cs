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
    public interface IDictionaryiFacade
    {
        IGetGenderService GetGenderService { get; }
        IAddOrUpdateGenderService AddOrUpdateGenderService { get; }
        IGetGendersService GetGendersService { get; }
        IDeleteGenerService DeleteGenerService { get; }

        IAddOrUpdateDepartmentService AddOrUpdateDepartmentService { get; }
        IDeleteDepartmentService DeleteDepartmentService { get; }
        IGetDepartmentService GetDepartmentService { get; }
        IGetDepartmentsService GetDepartmentsService { get; }
    }
}
