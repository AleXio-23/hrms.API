using hrms.Domain.Models.Dictionary.Locations;
using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.City.GetCities
{
    public interface IGetCitiesService
    {
        Task<ServiceResult<List<CityDTO>>> Execute(CitiesFilter filter, CancellationToken cancellationToken);
    }
}
