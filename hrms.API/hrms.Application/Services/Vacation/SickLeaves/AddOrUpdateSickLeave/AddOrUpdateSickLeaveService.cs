using AutoMapper;
using hrms.Application.Services.Dictionaries.Vacations.CompanyHolidays.GetCompanyHolidays;
using hrms.Application.Services.Dictionaries.WeekWorkingDays.GetWeekWorkingDay;
using hrms.Application.Services.User.UsersWorkSchedule.GetUsersWorkSchedules;
using hrms.Application.Services.Vacation.CheckAnyRegisteredHolidaysInRange;
using hrms.Application.Services.Vacation.SickLeaves.GetCurrentActiveSickLeaves;
using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Domain.Models.User;
using hrms.Domain.Models.Vacations.SickLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Constants;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.SickLeaves.AddOrUpdateSickLeave
{
    public class AddOrUpdateSickLeaveService : IAddOrUpdateSickLeaveService
    {
        private readonly IRepository<SickLeaf> _sickLeaveRepositroy;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IGetUsersWorkSchedulesService _getUsersWorkSchedulesService;
        private readonly IGetCompanyHolidaysService _getCompanyHolidaysService;
        private readonly IGetWeekWorkingDayService _getWeekWorkingDayService;
        private readonly IGetCurrentActiveSickLeavesService _getCurrentActiveSickLeavesService;
        private readonly ICheckAnyRegisteredHolidaysInRangeService _checkAnyRegisteredHolidaysInRangeService;
        private readonly IMapper _mapper;

        public AddOrUpdateSickLeaveService(IRepository<SickLeaf> sickLeaveRepositroy, IRepository<HolidayType> holidayTypeRepository, IGetUsersWorkSchedulesService getUsersWorkSchedulesService, IGetCompanyHolidaysService getCompanyHolidaysService, IGetWeekWorkingDayService getWeekWorkingDayService, IGetCurrentActiveSickLeavesService getCurrentActiveSickLeavesService, ICheckAnyRegisteredHolidaysInRangeService checkAnyRegisteredHolidaysInRangeService, IMapper mapper)
        {
            _sickLeaveRepositroy = sickLeaveRepositroy;
            _holidayTypeRepository = holidayTypeRepository;
            _getUsersWorkSchedulesService = getUsersWorkSchedulesService;
            _getCompanyHolidaysService = getCompanyHolidaysService;
            _getWeekWorkingDayService = getWeekWorkingDayService;
            _getCurrentActiveSickLeavesService = getCurrentActiveSickLeavesService;
            _checkAnyRegisteredHolidaysInRangeService = checkAnyRegisteredHolidaysInRangeService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SickLeaveDTO>> Execute(SickLeaveDTO sickLeaveDTO, CancellationToken cancellationToken)
        {
            var checkrangeOverlaps = await _checkAnyRegisteredHolidaysInRangeService
                .Execute(sickLeaveDTO.UserId,
                            sickLeaveDTO.DateStart,
                            sickLeaveDTO.DateEnd,
                            cancellationToken)
                .ConfigureAwait(false);
            if (checkrangeOverlaps == null || checkrangeOverlaps.Data == null)
            {
                throw new NullReferenceException("Error checking date overlaps for sick leave");
            }
            if (checkrangeOverlaps.Data.HasOverlap)
            {
                throw new ArgumentException($"Sick leave with in dates {sickLeaveDTO.DateStart.ToShortDateString()} - {sickLeaveDTO.DateEnd.ToShortDateString()} has overlap in dates with your registered {checkrangeOverlaps.Data.HolidayType} with date range {checkrangeOverlaps.Data.HolidayStart?.ToShortDateString()} - {checkrangeOverlaps.Data.HolidayEnd?.ToShortDateString()}");
            }
            var getHolidayTypeWithRangeType = await _holidayTypeRepository
            .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == VacationContants.HolidayCode_SickLeave).FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false) ?? throw new NotFoundException($"Holiday type for sick leave not found");

            if (getHolidayTypeWithRangeType.HolidayRangeType == null)
            {
                throw new NotFoundException($"Holiday range type for sick leave not found");
            }

            // Create list for each day between sick leave start and end dates
            var datetimeListOfSickLeaveExcludeCompanyOrUserHolidayDates = new List<DateTime>();
            var datetimeListOfSickLeaveDaysRange = new List<DateTime>
            {
                sickLeaveDTO.DateStart
            };

            for (DateTime date = sickLeaveDTO.DateStart.AddDays(1); date < sickLeaveDTO.DateEnd; date = date.AddDays(1))
            {
                datetimeListOfSickLeaveDaysRange.Add(date);
            }
            datetimeListOfSickLeaveDaysRange.Add(sickLeaveDTO.DateEnd);

            //Get Users working days  for check, if user not working on some day, not to count it in sick leave summed days
            var filterForUserSchedules = new UsersWorkScheduleFilter()
            {
                UserId = sickLeaveDTO.UserId
            };
            var userWorkingDays = await _getUsersWorkSchedulesService.Execute(filterForUserSchedules, cancellationToken).ConfigureAwait(false);
            if (userWorkingDays.ErrorOccured || userWorkingDays?.Data == null || userWorkingDays?.Data?.Count < 1)
            {
                throw new ArgumentException($"Working days for user with id {sickLeaveDTO.UserId} is not configured");
            }

            //Get company holiday dates also not to include them in sick leave summed days
            var filterForCompanyHolidays = new CompanyHolidayFilter()
            {
                EventDateStart = sickLeaveDTO.DateStart,
                EventDateEnd = sickLeaveDTO.DateEnd,
                IsActive = true
            };

            var getCompanyHoldayDatesBetweenSickLeaveStartAndEndDates = await _getCompanyHolidaysService.Execute(filterForCompanyHolidays, cancellationToken).ConfigureAwait(false);

            // For each user date, calculate if day is working for user or not based on getCompanyHoldayDatesBetweenPayedLeaveStartAndEndDates and userWorkingDays
            foreach (var vacDate in datetimeListOfSickLeaveDaysRange)
            {
                string weekDayName = vacDate.ToString("dddd").ToLower();
                var getWeekDayId = await _getWeekWorkingDayService.Execute(weekDayName, cancellationToken).ConfigureAwait(false);
                var checkIfUserWorkingDaysContainThisDate = userWorkingDays?.Data.Any(x => x.WeekWorkingDayId == getWeekDayId?.Data?.Id) ?? false;

                var checkIfDateIsInCompanyHolidayDates = getCompanyHoldayDatesBetweenSickLeaveStartAndEndDates?.Data?.Any(x => x.EventDate?.Date == vacDate.Date) ?? false;
                if (checkIfUserWorkingDaysContainThisDate && !checkIfDateIsInCompanyHolidayDates)
                {
                    datetimeListOfSickLeaveExcludeCompanyOrUserHolidayDates.Add(vacDate);
                }
            }
            //Count leave days excluding company holdays or days when user not working
            sickLeaveDTO.CountDays = datetimeListOfSickLeaveExcludeCompanyOrUserHolidayDates.Count;

            //Get User active days count for sick leave (Includes additional days from previous quarter/year)
            var getUserAccessibleFreeDaysForSickLeave = await _getCurrentActiveSickLeavesService.Execute(sickLeaveDTO.UserId, cancellationToken).ConfigureAwait(false);
            if (getUserAccessibleFreeDaysForSickLeave == null || getUserAccessibleFreeDaysForSickLeave.Data == null)
            {
                throw new NotFoundException("Error getting user's accesible free days for leave");
            }

            var sumDays = getUserAccessibleFreeDaysForSickLeave.Data.LeftLeaveDays ?? 0 + getUserAccessibleFreeDaysForSickLeave.Data.RemainingAvailableDaysFromPastQuarterOrYear ?? 0;
            if (sickLeaveDTO.CountDays > sumDays)
            {
                //დასამატებელია, თუ მომხმარებელს გაუვიდა ანაზღარებადი ავადმყოფობის დღეების რაოდენობა, დაემატოს არაანაზღაურებადი მისი ნებართვით (შეიძლება ფრონტის ნაწილშიც მოგვარდეს ეგ)
                throw new ArgumentException($"User with id {sickLeaveDTO.UserId} can't register sick leave ticket. Your request is for {sickLeaveDTO.CountDays} day(s) holiday while your access days for sick leaves are {sumDays}(Including access days from previous quarter/year).");
            }

            //If its new request, add new
            if (sickLeaveDTO.Id == null || sickLeaveDTO.Id < 0)
            {
                var newSickLeave = new SickLeaf()
                {
                    UserId = sickLeaveDTO.UserId,
                    DateStart = sickLeaveDTO.DateStart,
                    DateEnd = sickLeaveDTO.DateEnd,
                    CountDays = sickLeaveDTO.CountDays ?? 0,
                    Approved = null,
                    ApprovedByUserId = null,
                    Comment = null
                };

                var registerResult = await _sickLeaveRepositroy.Add(newSickLeave, cancellationToken).ConfigureAwait(false);
                var registerDtoResult = _mapper.Map<SickLeaveDTO>(registerResult);

                return ServiceResult<SickLeaveDTO>.SuccessResult(registerDtoResult);
            }
            //Check if its editing existing payed leave request
            else
            {
                var getExistingRegisteredSickLeave = await _sickLeaveRepositroy
                .Get(sickLeaveDTO.Id ?? -1, cancellationToken)
                    .ConfigureAwait(false) ?? throw new NotFoundException($"Registered sick leave request on id {sickLeaveDTO.Id} not found");

                getExistingRegisteredSickLeave.DateStart = sickLeaveDTO.DateStart;
                getExistingRegisteredSickLeave.DateEnd = sickLeaveDTO.DateEnd;
                getExistingRegisteredSickLeave.CountDays = sickLeaveDTO.CountDays ?? 0;
                getExistingRegisteredSickLeave.Approved = null;
                getExistingRegisteredSickLeave.ApprovedByUserId = null;
                getExistingRegisteredSickLeave.Comment = null;

                var updateResult = await _sickLeaveRepositroy.Update(getExistingRegisteredSickLeave, cancellationToken).ConfigureAwait(false);
                var updateDtoResult = _mapper.Map<SickLeaveDTO>(updateResult);

                return ServiceResult<SickLeaveDTO>.SuccessResult(updateDtoResult);
            }

        }
    }
}
