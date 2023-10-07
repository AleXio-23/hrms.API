using hrms.Domain.Models.Vacations.Location;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.State.AddOrUpdateState
{
    public interface IAddOrUpdateStateService
    {
        Task<ServiceResult<StateDTO>> Execute(StateDTO stateDTO, CancellationToken cancellationToken);
    }
}
