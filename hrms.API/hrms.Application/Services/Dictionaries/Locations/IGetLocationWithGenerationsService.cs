using hrms.Domain.Models.Dictionary;
using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations
{
    public interface IGetLocationWithGenerationsService
    {
        Task<ServiceResult<IEnumerable<DefaultMultiLevelDictionaryResponse>>> Execute(CancellationToken cancellation);
    }
}
