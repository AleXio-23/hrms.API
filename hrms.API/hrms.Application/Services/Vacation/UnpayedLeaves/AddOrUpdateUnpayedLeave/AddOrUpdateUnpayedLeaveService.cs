using AutoMapper;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDay;
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules;
using hrms.Application.Services.Vacation.CheckAnyRegisteredHolidaysInRange;
using hrms.Application.Services.Vacation.UnpayedLeaves.GetCurrentActiveUnpayedLeaves;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Domain.Models.User;
using hrms.Domain.Models.Vacations.UnpayedLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.UnpayedLeaves.AddOrUpdateUnpayedLeave
{
    public class AddOrUpdateUnpayedLeaveService : IAddOrUpdateUnpayedLeaveService
    {
        private readonly IRepository<UnpayedLeaf> _unPayedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IGetUsersWorkSchedulesService _getUsersWorkSchedulesService;
        private readonly IGetCompanyHolidaysService _getCompanyHolidaysService;
        private readonly IGetWeekWorkingDayService _getWeekWorkingDayService;
        private readonly IGetCurrentActiveUnpayedLeavesService _getCurrentActiveUnpayedLeavesService;
        private readonly ICheckAnyRegisteredHolidaysInRangeService _checkAnyRegisteredHolidaysInRangeService;
        private readonly IMapper _mapper;

        public AddOrUpdateUnpayedLeaveService(IRepository<UnpayedLeaf> unPayedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IGetUsersWorkSchedulesService getUsersWorkSchedulesService, IGetCompanyHolidaysService getCompanyHolidaysService, IGetWeekWorkingDayService getWeekWorkingDayService, IGetCurrentActiveUnpayedLeavesService getCurrentActiveUnpayedLeavesService, ICheckAnyRegisteredHolidaysInRangeService checkAnyRegisteredHolidaysInRangeService, IMapper mapper)
        {
            _unPayedLeaveRepository = unPayedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _getUsersWorkSchedulesService = getUsersWorkSchedulesService;
            _getCompanyHolidaysService = getCompanyHolidaysService;
            _getWeekWorkingDayService = getWeekWorkingDayService;
            _getCurrentActiveUnpayedLeavesService = getCurrentActiveUnpayedLeavesService;
            _checkAnyRegisteredHolidaysInRangeService = checkAnyRegisteredHolidaysInRangeService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UnpayedLeaveDTO>> Execute(UnpayedLeaveDTO unpayedLeaveDTO, CancellationToken cancellationToken)
        {
            var checkrangeOverlaps = await _checkAnyRegisteredHolidaysInRangeService.Execute(unpayedLeaveDTO.UserId, unpayedLeaveDTO.DateStart, unpayedLeaveDTO.DateEnd, cancellationToken).ConfigureAwait(false);
            if (checkrangeOverlaps == null || checkrangeOverlaps.Data == null)
            {
                throw new NullReferenceException("Error checking date overlaps for payed leave");
            }
            if (checkrangeOverlaps.Data.HasOverlap)
            {
                throw new ArgumentException($"Unpayed leave with in dates {unpayedLeaveDTO.DateStart.ToShortDateString()} - {unpayedLeaveDTO.DateEnd.ToShortDateString()} has overlap in dates with your registered {checkrangeOverlaps.Data.HolidayType} with date range {checkrangeOverlaps.Data.HolidayStart?.ToShortDateString()} - {checkrangeOverlaps.Data.HolidayEnd?.ToShortDateString()}");
            }

            var getHolidayTypeWithRangeType = await _holidayTypeRepository
                .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == "unpayed_leave").FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false) ?? throw new NotFoundException($"Holiday type for unpayed leave not found");

            if (getHolidayTypeWithRangeType.HolidayRangeType == null)
            {
                throw new NotFoundException($"Holiday range type for unpayed leave not found");
            }

            // Create list for each day between unpayed leave start and end dates
            var datetimeListOfUnpayedLeaveExcludeCompanyOrUserHolidayDates = new List<DateTime>();
            var datetimeListOfUnpayedLeaveDaysRange = new List<DateTime>
            {
                unpayedLeaveDTO.DateStart
            };

            for (DateTime date = unpayedLeaveDTO.DateStart.AddDays(1); date < unpayedLeaveDTO.DateEnd; date = date.AddDays(1))
            {
                datetimeListOfUnpayedLeaveDaysRange.Add(date);
            }
            datetimeListOfUnpayedLeaveDaysRange.Add(unpayedLeaveDTO.DateEnd);

            //Get Users working days  for check, if user not working on some day, not to count it in unpayed leave summed days
            var filterForUserSchedules = new UsersWorkScheduleFilter()
            {
                UserId = unpayedLeaveDTO.UserId
            };
            var userWorkingDays = await _getUsersWorkSchedulesService.Execute(filterForUserSchedules, cancellationToken).ConfigureAwait(false);
            if (userWorkingDays.ErrorOccured || userWorkingDays?.Data == null || userWorkingDays?.Data?.Count < 1)
            {
                throw new ArgumentException($"Working days for user with id {unpayedLeaveDTO.UserId} is not configured");
            }

            //Get company holiday dates also not to include them in payed leave summed days
            var filterForCompanyHolidays = new CompanyHolidayFilter()
            {
                EventDateStart = unpayedLeaveDTO.DateStart,
                EventDateEnd = unpayedLeaveDTO.DateEnd,
                IsActive = true
            };

            var getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates = await _getCompanyHolidaysService.Execute(filterForCompanyHolidays, cancellationToken).ConfigureAwait(false);

            // For each user date, calculate if day is working for user or not based on getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates and userWorkingDays
            foreach (var vacDate in datetimeListOfUnpayedLeaveDaysRange)
            {
                string weekDayName = vacDate.ToString("dddd").ToLower();
                var getWeekDayId = await _getWeekWorkingDayService.Execute(weekDayName, cancellationToken).ConfigureAwait(false);
                var checkIfUserWorkingDaysContainThisDate = userWorkingDays?.Data.Any(x => x.WeekWorkingDayId == getWeekDayId?.Data?.Id) ?? false;

                var checkIfDateIsInCompanyHolidayDates = getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates?.Data?.Any(x => x.EventDate?.Date == vacDate.Date) ?? false;
                if (checkIfUserWorkingDaysContainThisDate && !checkIfDateIsInCompanyHolidayDates)
                {
                    datetimeListOfUnpayedLeaveExcludeCompanyOrUserHolidayDates.Add(vacDate);
                }
            }
            //Count leave days excluding company holdays or days when user not working
            unpayedLeaveDTO.CountDays = datetimeListOfUnpayedLeaveExcludeCompanyOrUserHolidayDates.Count;

            //Get User active days count for unpayed leave (Includes additional days from previous quarter/year)
            var getUserAccessibleFreeDaysForUnpayedLeave = await _getCurrentActiveUnpayedLeavesService.Execute(unpayedLeaveDTO.UserId, cancellationToken).ConfigureAwait(false);
            if (getUserAccessibleFreeDaysForUnpayedLeave == null || getUserAccessibleFreeDaysForUnpayedLeave.Data == null)
            {
                throw new NotFoundException("Error getting user's accesible free days for leave");
            }

            var sumDays = getUserAccessibleFreeDaysForUnpayedLeave.Data.LeftLeaveDays ?? 0 + getUserAccessibleFreeDaysForUnpayedLeave.Data.RemainingAvailableDaysFromPastQuarterOrYear ?? 0;
            if (unpayedLeaveDTO.CountDays > sumDays)
            {
                throw new ArgumentException($"User with id {unpayedLeaveDTO.UserId} can't register unpayed leave ticket. Your request is for {unpayedLeaveDTO.CountDays} day(s) holiday while your access days for unpeayed leaves are {sumDays}(Including access days from previous quarter/year).");
            }

            //If its new request, add new
            if (unpayedLeaveDTO.Id == null || unpayedLeaveDTO.Id < 0)
            {
                var newunPayedLeave = new UnpayedLeaf()
                {
                    UserId = unpayedLeaveDTO.UserId,
                    DateStart = unpayedLeaveDTO.DateStart,
                    DateEnd = unpayedLeaveDTO.DateEnd,
                    CountDays = unpayedLeaveDTO.CountDays,
                    Approved = null,
                    ApprovedByUserId = null,
                    Comment = null
                };

                var registerResult = await _unPayedLeaveRepository.Add(newunPayedLeave, cancellationToken).ConfigureAwait(false);
                var registerDtoResult = _mapper.Map<UnpayedLeaveDTO>(registerResult);

                return ServiceResult<UnpayedLeaveDTO>.SuccessResult(registerDtoResult);
            }
            //Check if its editing existing payed leave request
            else
            {
                var getExistingRegisteredunPayedLeave = await _unPayedLeaveRepository
                    .Get(unpayedLeaveDTO.Id ?? -1, cancellationToken)
                    .ConfigureAwait(false) ?? throw new NotFoundException($"Registered unpayed leave request on id {unpayedLeaveDTO.Id} not found");

                getExistingRegisteredunPayedLeave.DateStart = unpayedLeaveDTO.DateStart;
                getExistingRegisteredunPayedLeave.DateEnd = unpayedLeaveDTO.DateEnd;
                getExistingRegisteredunPayedLeave.CountDays = unpayedLeaveDTO.CountDays;
                getExistingRegisteredunPayedLeave.Approved = null;
                getExistingRegisteredunPayedLeave.ApprovedByUserId = null;
                getExistingRegisteredunPayedLeave.Comment = null;

                var updateResult = await _unPayedLeaveRepository.Update(getExistingRegisteredunPayedLeave, cancellationToken).ConfigureAwait(false);
                var updateDtoResult = _mapper.Map<UnpayedLeaveDTO>(updateResult);

                return ServiceResult<UnpayedLeaveDTO>.SuccessResult(updateDtoResult);
            }
        }
    }
}
