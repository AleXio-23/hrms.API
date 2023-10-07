using hrms.Domain.Models.Dictionary.Locations;
using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.State.GetStates
{
    public interface IGetStatesService
    {
        Task<ServiceResult<List<StateDTO>>> Execute(StatesFilter filter, CancellationToken cancellationToken);
    }
}
