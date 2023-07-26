using hrms.Domain.Models.Vacations.CheckAnyRegisteredHolidaysInRange;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.CheckAnyRegisteredHolidaysInRange
{
    public class CheckAnyRegisteredHolidaysInRangeService : ICheckAnyRegisteredHolidaysInRangeService
    {
        private readonly IRepository<PayedLeaf> _payedLeaveRepository;
        private readonly IRepository<UnpayedLeaf> _unPayedLeaveRepository;
        private readonly IRepository<SickLeaf> _sickLaeveRepository;
        private readonly IRepository<DayOff> _dayOffRepository;

        public CheckAnyRegisteredHolidaysInRangeService(IRepository<PayedLeaf> payedLeaveRepository, IRepository<UnpayedLeaf> unPayedLeaveRepository, IRepository<SickLeaf> sickLaeveRepository, IRepository<DayOff> dayOffRepository)
        {
            _payedLeaveRepository = payedLeaveRepository;
            _unPayedLeaveRepository = unPayedLeaveRepository;
            _sickLaeveRepository = sickLaeveRepository;
            _dayOffRepository = dayOffRepository;
        }

        public async Task<ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>> Execute(int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var response = new CheckAnyRegisteredHolidaysInRangeServiceResponse();
            //Check for payearLeafes
            var hasOverlapWithPayedLeave = await _payedLeaveRepository.Where(x =>
                        x.UserId == userId &&
                        (startDate.Date >= x.DateStart.Date && startDate.Date <= x.DateEnd.Date || // Check if startDate is within Payed leaf range
                         endDate.Date >= x.DateStart.Date && endDate.Date <= x.DateEnd.Date ||     // Check if endDate is within Payed leaf range
                         startDate.Date <= x.DateStart.Date && endDate.Date >= x.DateEnd.Date)     // Check if Payed leaf range is within startDate and endDate
                        ).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (hasOverlapWithPayedLeave != null)
            {
                response = new CheckAnyRegisteredHolidaysInRangeServiceResponse()
                {
                    HasOverlap = true,
                    HolidayType = "Payed leave",
                    HolidayStart = hasOverlapWithPayedLeave.DateStart,
                    HolidayEnd = hasOverlapWithPayedLeave.DateEnd
                };
                return ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>.SuccessResult(response);
            }

            var hasOverlapWithUnpayedLeave = await _unPayedLeaveRepository.Where(x =>
                        x.UserId == userId &&
                        (startDate.Date >= x.DateStart.Date && startDate.Date <= x.DateEnd.Date || // Check if startDate is within Payed leaf range
                        endDate.Date >= x.DateStart.Date && endDate.Date <= x.DateEnd.Date ||     // Check if endDate is within Payed leaf range
                        startDate.Date <= x.DateStart.Date && endDate.Date >= x.DateEnd.Date)     // Check if Payed leaf range is within startDate and endDate
                        ).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (hasOverlapWithUnpayedLeave != null)
            {
                response = new CheckAnyRegisteredHolidaysInRangeServiceResponse()
                {
                    HasOverlap = true,
                    HolidayType = "Unpayed leave",
                    HolidayStart = hasOverlapWithUnpayedLeave.DateStart,
                    HolidayEnd = hasOverlapWithUnpayedLeave.DateEnd
                };
                return ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>.SuccessResult(response);
            }

            var hasOverlapWithSickLeave = await _sickLaeveRepository.Where(x =>
                        x.UserId == userId &&
                        (startDate.Date >= x.DateStart.Date && startDate.Date <= x.DateEnd.Date || // Check if startDate is within Payed leaf range
                         endDate.Date >= x.DateStart.Date && endDate.Date <= x.DateEnd.Date ||     // Check if endDate is within Payed leaf range
                        startDate.Date <= x.DateStart.Date && endDate.Date >= x.DateEnd.Date)     // Check if Payed leaf range is within startDate and endDate
                        ).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (hasOverlapWithSickLeave != null)
            {
                response = new CheckAnyRegisteredHolidaysInRangeServiceResponse()
                {
                    HasOverlap = true,
                    HolidayType = "Sick leave",
                    HolidayStart = hasOverlapWithSickLeave.DateStart,
                    HolidayEnd = hasOverlapWithSickLeave.DateEnd
                };
                return ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>.SuccessResult(response);
            }

            var hasOverlapWithDayOff = await _dayOffRepository.Where(x =>
                        x.UserId == userId &&
                        (startDate.Date >= x.Date.Date && startDate.Date <= x.Date.Date || // Check if startDate is the same as the DayOff date
                        endDate.Date >= x.Date.Date && endDate.Date <= x.Date.Date ||     // Check if endDate is the same as the DayOff date
                        startDate.Date <= x.Date.Date && endDate.Date >= x.Date.Date)     // Check if DayOff date is within startDate and endDate
                        ).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

            if (hasOverlapWithDayOff != null)
            {
                response = new CheckAnyRegisteredHolidaysInRangeServiceResponse()
                {
                    HasOverlap = true,
                    HolidayType = "Payed leave",
                    HolidayStart = hasOverlapWithDayOff.Date,
                    HolidayEnd = null
                };
                return ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>.SuccessResult(response);
            }


            response = new CheckAnyRegisteredHolidaysInRangeServiceResponse()
            {
                HasOverlap = false,
                HolidayType = null,
                HolidayStart = null,
                HolidayEnd = null
            };

            return ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>.SuccessResult(response);
        }
    }
}
