using AutoMapper;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDay;
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules;
using hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Domain.Models.User;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave
{
    public class AddOrUpdatePayedLeaveService : IAddOrUpdatePayedLeaveService
    {
        private readonly IRepository<PayedLeaf> _payedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IGetUsersWorkSchedulesService _getUsersWorkSchedulesService;
        private readonly IGetCompanyHolidaysService _getCompanyHolidaysService;
        private readonly IGetWeekWorkingDayService _getWeekWorkingDayService;
        private readonly IGetCurrentActivePayedLeavesService _getCurrentActivePayedLeavesService;
        private readonly IMapper _mapper;

        public AddOrUpdatePayedLeaveService(IRepository<PayedLeaf> payedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IGetUsersWorkSchedulesService getUsersWorkSchedulesService, IGetCompanyHolidaysService getCompanyHolidaysService, IGetWeekWorkingDayService getWeekWorkingDayService, IGetCurrentActivePayedLeavesService getCurrentActivePayedLeavesService, IMapper mapper)
        {
            _payedLeaveRepository = payedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _getUsersWorkSchedulesService = getUsersWorkSchedulesService;
            _getCompanyHolidaysService = getCompanyHolidaysService;
            _getWeekWorkingDayService = getWeekWorkingDayService;
            _getCurrentActivePayedLeavesService = getCurrentActivePayedLeavesService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PayedLeaveDTO>> Execute(PayedLeaveDTO payedLeaveDTO, CancellationToken cancellationToken)
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
                var getWeekDayId = await _getWeekWorkingDayService.Execute(weekDayName, cancellationToken).ConfigureAwait(false);
                var checkIfUserWorkingDaysContainThisDate = userWorkingDays?.Data.Any(x => x.WeekWorkingDayId == getWeekDayId?.Data?.Id) ?? false;

                var checkIfDateIsInCompanyHolidayDates = getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates?.Data?.Any(x => x.EventDate?.Date == vacDate.Date) ?? false;
                if (checkIfUserWorkingDaysContainThisDate && !checkIfDateIsInCompanyHolidayDates)
                {
                    datetimeListOfPayedLeaveExcludeCompanyOrUserHolidayDates.Add(vacDate);
                }
            }
            //Count leave days excluding company holdays or days when user not working
            payedLeaveDTO.CountDays = datetimeListOfPayedLeaveExcludeCompanyOrUserHolidayDates.Count;

            //Get User active days count for payed leave (Includes additional days from previous quarter/year)
            var getUserAccessibleFreeDaysForPayedLeave = await _getCurrentActivePayedLeavesService.Execute(payedLeaveDTO.UserId, cancellationToken).ConfigureAwait(false);
            if (getUserAccessibleFreeDaysForPayedLeave == null || getUserAccessibleFreeDaysForPayedLeave.Data == null)
            {
                throw new NotFoundException("Error getting user's accesible free days for leave");
            }

            var sumDays = getUserAccessibleFreeDaysForPayedLeave.Data.LeftPayedLeavesDays ?? 0 + getUserAccessibleFreeDaysForPayedLeave.Data.RemainingAvailableDaysFromPastQuarterOrYear ?? 0;
            if (payedLeaveDTO.CountDays > sumDays)
            {
                throw new ArgumentException($"User with id {payedLeaveDTO.UserId} can't register payed leave ticket. Your request {payedLeaveDTO.CountDays} day(s) holiday while your access days for peayed leaves are {sumDays}(Including access days from previous quarter/year).");
            }

            var newPayedLeave = new PayedLeaf()
            {
                UserId = payedLeaveDTO.UserId,
                DateStart = payedLeaveDTO.DateStart,
                DateEnd = payedLeaveDTO.DateEnd,
                CountDays = payedLeaveDTO.CountDays,
                PayBeforeHeadEnabled = payedLeaveDTO.PayBeforeHeadEnabled,
                Approved = null,
                ApprovedByUserId = null,
                Comment = null
            };

            var registerResult = await _payedLeaveRepository.Add(newPayedLeave, cancellationToken).ConfigureAwait(false);
            var registerDtoResult = _mapper.Map<PayedLeaveDTO>(registerResult);

            return ServiceResult<PayedLeaveDTO>.SuccessResult(registerDtoResult);
        }
    }
}
