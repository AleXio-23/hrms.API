using hrms.Persistance.Entities;

namespace hrms.Application.Services.Vacation.QuartersCounts
{
    public interface IQuartersCountsService
    {
        (DateTime startDate, DateTime EndDate) CalculateQuarterStartAndEndDate(QuartersConfiguration quarterCfg, bool isConsideredForNextYearRange = false);
        Task<(QuartersConfiguration quartersConfiguration, DateTime? PreviousQuarterStart, DateTime? previousQuarterEnd)> GetPreviousQuarterWithDateRanges(CancellationToken cancellationToken);
        Task<QuartersConfiguration> GetCurrentQuarter(CancellationToken cancellationToken);
    }
}
