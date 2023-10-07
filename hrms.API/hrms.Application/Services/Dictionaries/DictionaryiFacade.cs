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
using hrms.Application.Services.Dictionaries.Locations;
using hrms.Application.Services.Dictionaries.Locations.City.GeCities;
using hrms.Application.Services.Dictionaries.Locations.Country.AddorUpdateCountry;
using hrms.Application.Services.Dictionaries.Locations.Country.DeleteCountry;
using hrms.Application.Services.Dictionaries.Locations.Country.GetCountries;
using hrms.Application.Services.Dictionaries.Locations.Country.GetCountry;
using hrms.Application.Services.Dictionaries.Locations.State.AddOrUpdateState;
using hrms.Application.Services.Dictionaries.Locations.State.GetStates;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.AddOrUpdateCompanyHolidays;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.DeleteCompanyHoliday;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHoliday;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeType;
using hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeTypes;
using hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayType;
using hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayTypes;
using hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDay;
using hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDays;

namespace hrms.Application.Services.Dictionaries
{
    public class DictionaryiFacade : IDictionaryiFacade
    {
        public DictionaryiFacade(IGetGenderService getGenderService, IAddOrUpdateGenderService addOrUpdateGenderService, IGetGendersService getGendersService, IDeleteGenerService deleteGenerService, IAddOrUpdateDepartmentService addOrUpdateDepartmentService, IDeleteDepartmentService deleteDepartmentService, IGetDepartmentService getDepartmentService, IGetDepartmentsService getDepartmentsService, IAddOrUpdateJobPositionService addOrUpdateJobPositionService, IDeleteJobPositionService deleteJobPositionService, IGetJobPositionService getJobPositionService, IGetJobPositionsService getJobPositionsService, IAddOrUpdateCompanyHolidaysService addOrUpdateCompanyHolidaysService, IDeleteCompanyHolidayService deleteCompanyHolidayService, IGetCompanyHolidayService getCompanyHolidayService, IGetCompanyHolidaysService getCompanyHolidaysService, IGetHolidayRangeTypeService getHolidayRangeTypeService, IGetHolidayRangeTypesService getHolidayRangeTypesService, IGetHolidayTypeService getHolidayTypeService, IGetHolidayTypesService getHolidayTypesService, IGetWeekWorkingDayService getWeekWorkingDayService, IGetWeekWorkingDaysService getWeekWorkingDaysService, IGetLocationWithGenerationsService getLocationWithGenerationsService, IAddorUpdateCountryService addorUpdateCountryService, IDeleteCountryService deleteCountryService, IGetCountryService getCountryService, IGetCountriesService getCountriesService, IAddOrUpdateStateService addOrUpdateStateService, IGetStatesService getStatesService, IGeCitiesService geCitiesService)
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
            GetHolidayTypeService = getHolidayTypeService;
            GetHolidayTypesService = getHolidayTypesService;
            GetWeekWorkingDayService = getWeekWorkingDayService;
            GetWeekWorkingDaysService = getWeekWorkingDaysService;
            GetLocationWithGenerationsService = getLocationWithGenerationsService;
            AddorUpdateCountryService = addorUpdateCountryService;
            DeleteCountryService = deleteCountryService;
            GetCountryService = getCountryService;
            GetCountriesService = getCountriesService;
            AddOrUpdateStateService = addOrUpdateStateService;
            GetStatesService = getStatesService;
            GeCitiesService = geCitiesService;
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

        public IGetHolidayTypeService GetHolidayTypeService { get; }

        public IGetHolidayTypesService GetHolidayTypesService { get; }

        public IGetWeekWorkingDayService GetWeekWorkingDayService { get; }

        public IGetWeekWorkingDaysService GetWeekWorkingDaysService { get; }

        public IGetLocationWithGenerationsService GetLocationWithGenerationsService { get; }

        public IAddorUpdateCountryService AddorUpdateCountryService { get; }

        public IDeleteCountryService DeleteCountryService { get; }

        public IGetCountryService GetCountryService { get; }

        public IGetCountriesService GetCountriesService { get; }

        public IAddOrUpdateStateService AddOrUpdateStateService { get; }

        public IGetStatesService GetStatesService { get; }

        public IGeCitiesService GeCitiesService { get; }
    }
}
