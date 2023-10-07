using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.DeleteCountry
{
    public interface IDeleteCountryService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
    }
}
