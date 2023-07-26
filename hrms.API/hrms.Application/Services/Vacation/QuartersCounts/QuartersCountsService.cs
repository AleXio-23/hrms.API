using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;

namespace hrms.Application.Services.Vacation.QuartersCounts
{
    public class QuartersCountsService : IQuartersCountsService
    {

        private readonly IRepository<QuartersConfiguration> _quarterCfgRepository;

        public QuartersCountsService(IRepository<QuartersConfiguration> quarterCfgRepository)
        {
            _quarterCfgRepository = quarterCfgRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quarterCfg"></param>
        /// <param name="isConsideredForNextYearRange">Consider that, if we want to calculate currnet on next quarter date range,
        ///    it must count range for current and next year, so apram will be TRUE,
        ///    but if we wwant to calculate past quarter date ranges, it must be FALSE
        /// </param>
        /// <returns></returns>
        public (DateTime startDate, DateTime EndDate) CalculateQuarterStartAndEndDate(QuartersConfiguration quarterCfg, bool isConsideredForNextYearRange = false)
        {
            int currentYear = DateTime.Now.Year;

            if (quarterCfg.QuarterStartsMonth <= 12 && quarterCfg.QuarterEndsMonth >= 1)
            {
                var currentquarterStartDate = new DateTime(isConsideredForNextYearRange ? currentYear : currentYear - 1, quarterCfg.QuarterStartsMonth, quarterCfg.QuarterStartsDay, 0, 0, 0, millisecond: 0);
                var currentquarterEndDate = new DateTime(isConsideredForNextYearRange ? currentYear + 1 : currentYear, quarterCfg.QuarterEndsMonth, quarterCfg.QuarterEndsDay, 0, 0, 0, millisecond: 0);
                return (currentquarterStartDate, currentquarterEndDate);
            }
            else
            {
                var currentquarterStartDate = new DateTime(currentYear, quarterCfg.QuarterStartsMonth, quarterCfg.QuarterStartsDay, 0, 0, 0, millisecond: 0);
                var currentquarterEndDate = new DateTime(currentYear, quarterCfg.QuarterEndsMonth, quarterCfg.QuarterEndsDay, 0, 0, 0, millisecond: 0);
                return (currentquarterStartDate, currentquarterEndDate);
            }
        }

        /// <summary>
        /// Get current Quarter
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<QuartersConfiguration> GetCurrentQuarter(CancellationToken cancellationToken)
        {
            var getQUarterCfg = await _quarterCfgRepository.GetAll(cancellationToken).ConfigureAwait(false);
            var getCurrentDateMonth = DateTime.Now.Month;
            var getCurrentDateDay = DateTime.Now.Day;

            var getCurrentQuarter = getQUarterCfg.FirstOrDefault(x =>
                (new DateTime(DateTime.Now.Year, x.QuarterStartsMonth, x.QuarterStartsDay) <= DateTime.Now) &&
                (new DateTime(DateTime.Now.Year, x.QuarterEndsMonth, x.QuarterEndsDay) >= DateTime.Now)
            ) ?? throw new NotFoundException("Can't find correct quarter");
            return getCurrentQuarter;
        }

        /// <summary>
        /// Get previouse quarter with date ranges
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(QuartersConfiguration quartersConfiguration, DateTime? PreviousQuarterStart, DateTime? previousQuarterEnd)> GetPreviousQuarterWithDateRanges(CancellationToken cancellationToken)
        {
            var currentQuarter = await GetCurrentQuarter(cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException("Error calculating Current Quarter");

            QuartersConfiguration? previousQuarter = null;
            DateTime? PreviousQuarterStartCalc = null;
            DateTime? previousQuarterEndCalc = null;
            var getCurrentYear = DateTime.Now.Year;


            var getPreviousQuarterCodeName = currentQuarter.CodeName == "Q1"
                ? "Q4"
                : (currentQuarter.CodeName == "Q2"
                    ? "Q1"
                    : (currentQuarter.CodeName == "Q3"
                        ? "Q2" : (currentQuarter.CodeName == "Q4"
                            ? "Q3" : throw new Exception("Unexpected error counting previuse quarter codename"))));

            previousQuarter = await _quarterCfgRepository.FirstOrDefaultAsync(x => x.CodeName == getPreviousQuarterCodeName, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record for quarter 4{getPreviousQuarterCodeName} not found");
            var (startDate, EndDate) = CalculateQuarterStartAndEndDate(previousQuarter, false);
            PreviousQuarterStartCalc = startDate;
            previousQuarterEndCalc = EndDate;

            return (previousQuarter, PreviousQuarterStartCalc, previousQuarterEndCalc);
        }
    }
}
