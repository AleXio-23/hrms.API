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
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.AddOrUpdateCompanyHolidays;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.DeleteCompanyHoliday;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHoliday;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeType;
using hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeTypes;

namespace hrms.Application.Services.Dictionaries
{
    public class DictionaryiFacade : IDictionaryiFacade
    {
        public DictionaryiFacade(IGetGenderService getGenderService, IAddOrUpdateGenderService addOrUpdateGenderService, IGetGendersService getGendersService, IDeleteGenerService deleteGenerService, IAddOrUpdateDepartmentService addOrUpdateDepartmentService, IDeleteDepartmentService deleteDepartmentService, IGetDepartmentService getDepartmentService, IGetDepartmentsService getDepartmentsService, IAddOrUpdateJobPositionService addOrUpdateJobPositionService, IDeleteJobPositionService deleteJobPositionService, IGetJobPositionService getJobPositionService, IGetJobPositionsService getJobPositionsService, IAddOrUpdateCompanyHolidaysService addOrUpdateCompanyHolidaysService, IDeleteCompanyHolidayService deleteCompanyHolidayService, IGetCompanyHolidayService getCompanyHolidayService, IGetCompanyHolidaysService getCompanyHolidaysService, IGetHolidayRangeTypeService getHolidayRangeTypeService, IGetHolidayRangeTypesService getHolidayRangeTypesService)
        {
            GetGenderService = getGenderService;
            AddOrUpdateGenderService = addOrUpdateGenderService;
            GetGendersService = getGendersService;
            DeleteGenerService = deleteGenerService;
            AddOrUpdateDepartmentService = addOrUpdateDepartmentService;
            DeleteDepartmentService = deleteDepartmentService;
            GetDepartmentService = getDepartmentService;
            GetDepartmentsService = getDepartmentsService;
            AddOrUpdateJobPositionService = addOrUpdateJobPositionService;
            DeleteJobPositionService = deleteJobPositionService;
            GetJobPositionService = getJobPositionService;
            GetJobPositionsService = getJobPositionsService;
            AddOrUpdateCompanyHolidaysService = addOrUpdateCompanyHolidaysService;
            DeleteCompanyHolidayService = deleteCompanyHolidayService;
            GetCompanyHolidayService = getCompanyHolidayService;
            GetCompanyHolidaysService = getCompanyHolidaysService;
            GetHolidayRangeTypeService = getHolidayRangeTypeService;
            GetHolidayRangeTypesService = getHolidayRangeTypesService;
        }

        public IGetGenderService GetGenderService { get; }
        public IAddOrUpdateGenderService AddOrUpdateGenderService { get; }
        public IGetGendersService GetGendersService { get; }

        public IDeleteGenerService DeleteGenerService { get; }

        public IAddOrUpdateDepartmentService AddOrUpdateDepartmentService { get; }

        public IDeleteDepartmentService DeleteDepartmentService { get; }

        public IGetDepartmentService GetDepartmentService { get; }

        public IGetDepartmentsService GetDepartmentsService { get; }

        public IAddOrUpdateJobPositionService AddOrUpdateJobPositionService{ get; }

        public IDeleteJobPositionService DeleteJobPositionService{ get; }

        public IGetJobPositionService GetJobPositionService{ get; }

        public IGetJobPositionsService GetJobPositionsService{ get; }

        public IAddOrUpdateCompanyHolidaysService AddOrUpdateCompanyHolidaysService { get; }

        public IDeleteCompanyHolidayService DeleteCompanyHolidayService { get; }

        public IGetCompanyHolidayService GetCompanyHolidayService { get; }

        public IGetCompanyHolidaysService GetCompanyHolidaysService { get; }

        public IGetHolidayRangeTypeService GetHolidayRangeTypeService { get; }

        public IGetHolidayRangeTypesService GetHolidayRangeTypesService { get; }
    }
}
