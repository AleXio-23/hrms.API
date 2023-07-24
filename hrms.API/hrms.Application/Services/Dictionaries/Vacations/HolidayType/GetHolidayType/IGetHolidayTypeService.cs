using hrms.Domain.Models.Dictionary.Vacations;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Vacations.HolidayType.GetHolidayType
{
    public interface IGetHolidayTypeService
    {
        Task<ServiceResult<HolidayTypeDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
