using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.AddorUpdateCountry
{
    public interface IAddorUpdateCountryService
    {
        Task<ServiceResult<CountryDTO>> Execute(CountryDTO countryDTO, CancellationToken cancellationToken);
    }
}
