using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayRangeType.GetHolidayRangeType
{
    public interface IGetHolidayRangeTypeService
    {
        Task<ServiceResult<HolidayRangeTypeDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
