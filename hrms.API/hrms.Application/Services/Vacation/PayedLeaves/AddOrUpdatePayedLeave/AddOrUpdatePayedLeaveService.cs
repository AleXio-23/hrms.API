using AutoMapper;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedule;
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Domain.Models.User;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave
{
    public class AddOrUpdatePayedLeaveService : IAddOrUpdatePayedLeaveService
    {
        private readonly IRepository<PayedLeaf> _payedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IRepository<QuartersConfiguration> _quarterCfgRepository;
        private readonly IGetUsersWorkSchedulesService _getUsersWorkSchedulesService;
        private readonly IGetCompanyHolidaysService _getCompanyHolidaysService;
        private readonly IMapper _mapper;

        public AddOrUpdatePayedLeaveService(IRepository<PayedLeaf> payedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IRepository<QuartersConfiguration> quarterCfgRepository, IGetUsersWorkSchedulesService getUsersWorkSchedulesService, IGetCompanyHolidaysService getCompanyHolidaysService, IMapper mapper)
        {
            _payedLeaveRepository = payedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _quarterCfgRepository = quarterCfgRepository;
            _getUsersWorkSchedulesService = getUsersWorkSchedulesService;
            _getCompanyHolidaysService = getCompanyHolidaysService;
            _mapper = mapper;
        }

        public async Task<PayedLeaveDTO> Execute(PayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken)
        {
            var getHolidayTypeWithRangeType = await _holidayTypeRepository
                .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == "payed_leave").FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false) ?? throw new NotFoundException($"Holiday type for peayed leave not found");

            if (getHolidayTypeWithRangeType.HolidayRangeType == null)
            {
                throw new NotFoundException($"Holiday range type for peayed leave not found");
            }

            // Create list for each day between payed leave start and end dates
            var datetimeListOfPayedLeaveExcludeCompanyOrUserHolidayDates = new List<DateTime>();
            var datetimeListOfPayedLeaveDaysRange = new List<DateTime>
            {
                payedLeaveDTO.DateStart
            };

            for (DateTime date = payedLeaveDTO.DateStart.AddDays(1); date < payedLeaveDTO.DateEnd; date = date.AddDays(1))
            {
                datetimeListOfPayedLeaveDaysRange.Add(date);
            }
            datetimeListOfPayedLeaveDaysRange.Add(payedLeaveDTO.DateEnd);

            //Get Users working days  for check, if user not working on some day, not to count it in payed leave summed days
            var filterForUserSchedules = new UsersWorkScheduleFilter()
            {
                UserId = payedLeaveDTO.UserId
            };
            var userWorkingDays = await _getUsersWorkSchedulesService.Execute(filterForUserSchedules, cancellationToken).ConfigureAwait(false);
            if (userWorkingDays.ErrorOccured || userWorkingDays?.Data == null || userWorkingDays?.Data?.Count < 1)
            {
                throw new ArgumentException($"Working days for user with id {payedLeaveDTO.UserId} is not configured");
            }


            //Get company holiday dates also not to include them in payed leave summed days
            var filterForCompanyHolidays = new CompanyHolidayFilter()
            {
                EventDateStart = payedLeaveDTO.DateStart,
                EventDateEnd = payedLeaveDTO.DateEnd,
                IsActive = true
            };

            var getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates = await _getCompanyHolidaysService.Execute(filterForCompanyHolidays, cancellationToken).ConfigureAwait(false);


            // For each user date, calculate if day is working for user or not based on getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates and userWorkingDays
            foreach (var vacDate in datetimeListOfPayedLeaveDaysRange)
            {
                string weekDayName = vacDate.ToString("dddd").ToLower();

            }

            if (getHolidayTypeWithRangeType.HolidayRangeType.Equals("per_quarter"))
            {
                return null;
            }
            else if (getHolidayTypeWithRangeType.HolidayRangeType.Equals("per_year"))
            {
                return null;
            }
            else
            {
                throw new Exception("Unexpected payed leave range type");
            }
        }



    }
}
