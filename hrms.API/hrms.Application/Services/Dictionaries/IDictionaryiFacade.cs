using hrms.Application.Services.Dictionaries.Departments.AddOrUpdateDepartment;
using hrms.Application.Services.Dictionaries.Departments.DeleteDepartment;
using hrms.Application.Services.Dictionaries.Departments.GetDepartment;
using hrms.Application.Services.Dictionaries.Departments.GetDepartments;
using hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender;
using hrms.Application.Services.Dictionaries.Gender.DeleteGender;
using hrms.Application.Services.Dictionaries.Gender.GetGender;
using hrms.Application.Services.Dictionaries.Gender.GetGenders;
using hrms.Application.Services.Dictionaries.JobPositions.AddOrUpdateJobPosition;
using hrms.Application.Services.Dictionaries.JobPositions.DeleteJobPosition;
using hrms.Application.Services.Dictionaries.JobPositions.GetJobPosition;
using hrms.Application.Services.Dictionaries.JobPositions.GetJobPositions;

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

        IAddOrUpdateJobPositionService AddOrUpdateJobPositionService { get; }
        IDeleteJobPositionService DeleteJobPositionService { get; }
        IGetJobPositionService GetJobPositionService { get; }
        IGetJobPositionsService GetJobPositionsService { get; }
    }
}
