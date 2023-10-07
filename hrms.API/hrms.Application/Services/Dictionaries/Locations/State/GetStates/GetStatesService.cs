using hrms.Domain.Models.Dictionary.Locations;
using hrms.Domain.Models.Vacations.Location;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Locations.State.GetStates
{
    public class GetStatesService : IGetStatesService
    { 
        private readonly IRepository<Persistance.Entities.State> _stateRepository;

        public GetStatesService(IRepository<Persistance.Entities.State> stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public async Task<ServiceResult<List<StateDTO>>> Execute(StatesFilter filter, CancellationToken cancellationToken)
        {
            var query = _stateRepository.GetAllAsQueryable();

            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.Code != null && x.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name != null && x.Name.Contains(filter.Name));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }
            if (filter.CountryId.HasValue)
            {
                query = query.Where(x => x.CountryId == filter.CountryId.Value);
            }

            var result = await query.Select(x => new StateDTO()
            {
                Id = x.Id,
                CountryId = x.CountryId,
                SortIndex = x.SortIndex,
                Code = x.Code,
                Name = x.Name,
                IsActive = x.IsActive

            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<StateDTO>>.SuccessResult(result ?? new List<StateDTO>());
        }
    }
}
