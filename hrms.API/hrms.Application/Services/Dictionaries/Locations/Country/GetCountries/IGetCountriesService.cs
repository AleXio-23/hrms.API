using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Dictionary.Locations;
using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.GetCountries
{
    public interface IGetCountriesService
    {
        Task<ServiceResult<List<CountryDTO>>> Execute(CountryFilter filter, CancellationToken cancellationToken);
    }
}
