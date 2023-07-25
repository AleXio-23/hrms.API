using AutoMapper;
using hrms.Domain.Models.Vacations.PayedLeave;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace hrms.Application.Services.Vacation.PayedLeaves.AddOrUpdatePayedLeave
{
    public class AddOrUpdatePayedLeaveService : IAddOrUpdatePayedLeaveService
    {
        private readonly IRepository<PayedLeaf> _payedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IRepository<QuartersConfiguration> _quarterCfgRepository;
        private readonly IMapper _mapper;

        public AddOrUpdatePayedLeaveService(IRepository<PayedLeaf> payedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IRepository<QuartersConfiguration> quarterCfgRepository, IMapper mapper)
        {
            _payedLeaveRepository = payedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _quarterCfgRepository = quarterCfgRepository;
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

            if (getHolidayTypeWithRangeType.HolidayRangeType.Equals("per_quarter"))
            {

            }
            else if (getHolidayTypeWithRangeType.HolidayRangeType.Equals("per_year"))
            {

            }
            else
            {
                throw new Exception("Unexpected payed leave range type");
            }


            return null;
        }


        private async Task<(int, int)> CalculateAvailablePayedLeaveDaysForUser(int userId, CancellationToken cancellationToken)
        {
            var holidayTypeCode = "payed_leave";
            var holidayType = await _holidayTypeRepository
                .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == holidayTypeCode)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);



            var currentYear = DateTime.Now.Year;
            var currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;

            bool? isQuarterRange =
                holidayType.HolidayRangeType.Code == "per_quarter" ?
                true : (holidayType.HolidayRangeType.Code == "per_year"
                        ? false : null);
            if (holidayType == null || holidayType.HolidayRangeType == null || isQuarterRange == null)
            {
                throw new NotFoundException($"Holiday type or range type for payed leave not found");
            }
            //var startDate = isQuarterRange ? holidayType.HolidayRangeType.QuarterStarts : new DateTime(currentYear, 1, 1);
            //var endDate = isQuarterRange ? holidayType.HolidayRangeType.QuarterEnds : new DateTime(currentYear, 12, 31);

            var getCurrentQuarter = await GetCurrentQuarter(cancellationToken).ConfigureAwait(false);
            var startDate = isQuarterRange == true ? getCurrentQuarter.QuarterStarts : new DateTime(currentYear, 1, 1);
            var endDate = isQuarterRange == true ? getCurrentQuarter.QuarterEnds : new DateTime(currentYear, 12, 31);

            var userPayedLeaves = await _payedLeaveRepository
                .Where(x => x.UserId == userId && x.DateStart >= startDate && x.DateEnd <= endDate)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            var totalUsedDays = userPayedLeaves.Sum(x => x.CountDays);
            var remainingAvailableDays = holidayType.CountUsageDaysPerRange - totalUsedDays;

            return (totalUsedDays, remainingAvailableDays);
        }


        private async Task<QuartersConfiguration> GetCurrentQuarter(CancellationToken cancellationToken)
        {
            var getQUarterCfg = await _quarterCfgRepository.GetAll(cancellationToken).ConfigureAwait(false);
            var getCurrentDate = DateTime.Now;

            var getCurrentQuarter = getQUarterCfg.FirstOrDefault(x => x.QuarterStarts >= getCurrentDate && x.QuarterEnds <= getCurrentDate) ?? throw new NotFoundException("Can't find correct quarter");
            return getCurrentQuarter;
        }

        public async Task<bool> IsPayedLeaveRequestWithinLimit(PayedLeaveDTO payedLeaveDTO, bool isQuarterRange, CancellationToken cancellationToken)
        {
            var availableDays = await CalculateAvailablePayedLeaveDaysForUser(payedLeaveDTO.UserId, cancellationToken).ConfigureAwait(false);

            if (payedLeaveDTO.CountDays > availableDays.Item2)
            {
                throw new ArgumentException($"You only left {availableDays.Item2}. You can't request payed leave for {payedLeaveDTO.CountDays} days");
            }

            if (isQuarterRange)
            {
                //var nextQuarterStartDate = holidayType.HolidayRangeType.QuarterEnds.AddDays(1);
                //var nextQuarterEndDate = nextQuarterStartDate.AddMonths(3).AddDays(-1);

                //if (payedLeaveDTO.DateStart < nextQuarterStartDate || payedLeaveDTO.DateEnd > nextQuarterEndDate)
                //{
                //    return false;
                //}
            }
            else
            {
                var nextYearStartDate = new DateTime(payedLeaveDTO.DateEnd.Year + 1, 1, 1);

                if (payedLeaveDTO.DateStart < nextYearStartDate)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
