using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeTypes
{
    public interface IGetHolidayRangeTypesService
    {
        Task<ServiceResult<List<HolidayRangeTypeDTO>>> Execute(CancellationToken cancellationToken);
    }
}
