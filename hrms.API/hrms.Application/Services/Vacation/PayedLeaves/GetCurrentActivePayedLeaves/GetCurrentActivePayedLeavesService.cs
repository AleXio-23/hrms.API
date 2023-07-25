using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Vacation.PayedLeaves.GetCurrentActivePayedLeaves
{
    public class GetCurrentActivePayedLeavesService : IGetCurrentActivePayedLeavesService
    {
        private readonly IRepository<PayedLeaf> _payedLeaveRepository;
        private readonly IRepository<HolidayType> _holidayTypeRepository;
        private readonly IRepository<QuartersConfiguration> _quarterCfgRepository;

        public GetCurrentActivePayedLeavesService(IRepository<PayedLeaf> payedLeaveRepository, IRepository<HolidayType> holidayTypeRepository, IRepository<QuartersConfiguration> quarterCfgRepository)
        {
            _payedLeaveRepository = payedLeaveRepository;
            _holidayTypeRepository = holidayTypeRepository;
            _quarterCfgRepository = quarterCfgRepository;
        }

        public async Task<string> Execute(int userId, CancellationToken cancellationToken)
        {
            //get payed leave holiday with its range type
            var holidayTypeCode = "payed_leave";
            var holidayType = await _holidayTypeRepository
                .GetIncluding(x => x.HolidayRangeType)
                .Where(x => x.Code == holidayTypeCode)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            if (holidayType == null || holidayType.HolidayRangeType == null)
            {
                throw new NotFoundException($"Holiday type or range type for payed leave not found");
            }
            //Check holidays range type, if its Per quarter or per year
            bool? isQuarterRange =
              holidayType?.HolidayRangeType.Code == "per_quarter" || (holidayType?.HolidayRangeType.Code == "per_year"
                      ? false : throw new NotFoundException("Holday range type not found"));
        }
    }
}
