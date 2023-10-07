using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.GetCountry
{
    public interface IGetCountryService
    {
        Task<ServiceResult<CountryDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
