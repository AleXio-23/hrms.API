using hrms.Domain.Models.Vacations.CheckAnyRegisteredHolidaysInRange;
using hrms.Shared.Models;

namespace hrms.Application.Services.Vacation.CheckAnyRegisteredHolidaysInRange
{
    public interface ICheckAnyRegisteredHolidaysInRangeService
    { 
        Task<ServiceResult<CheckAnyRegisteredHolidaysInRangeServiceResponse>> Execute(int userId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}
