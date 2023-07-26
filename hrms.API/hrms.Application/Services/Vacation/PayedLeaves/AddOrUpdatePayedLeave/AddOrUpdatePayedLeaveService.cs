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
