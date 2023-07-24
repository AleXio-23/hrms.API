using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayTypes
{
    public interface IGetHolidayTypesService
    {
        Task<ServiceResult<List<HolidayTypeDTO>>> Execute(CancellationToken cancellationToken);
    }
}
