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
using hrms.Application.Services.Dictionaries.Locations.Country.AddorUpdateCountry;
using hrms.Application.Services.Dictionaries.Locations.Country.DeleteCountry;
using hrms.Application.Services.Dictionaries.Locations.Country.GetCountries;
using hrms.Application.Services.Dictionaries.Locations.Country.GetCountry;
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

        IAddOrUpdateCompanyHolidaysService AddOrUpdateCompanyHolidaysService { get; }
        IDeleteCompanyHolidayService DeleteCompanyHolidayService { get; }
        IGetCompanyHolidayService GetCompanyHolidayService { get; }
        IGetCompanyHolidaysService GetCompanyHolidaysService { get; }

        IGetHolidayRangeTypeService GetHolidayRangeTypeService { get; }
        IGetHolidayRangeTypesService GetHolidayRangeTypesService { get; }

        IGetHolidayTypeService GetHolidayTypeService { get; }
        IGetHolidayTypesService GetHolidayTypesService { get; }

        IGetWeekWorkingDayService GetWeekWorkingDayService { get; }
        IGetWeekWorkingDaysService GetWeekWorkingDaysService { get; }

        IGetLocationWithGenerationsService GetLocationWithGenerationsService { get; }
        IAddorUpdateCountryService  AddorUpdateCountryService { get; }
        IDeleteCountryService DeleteCountryService { get; }
        IGetCountryService  GetCountryService { get; }
        IGetCountriesService GetCountriesService { get; }
    }
}
