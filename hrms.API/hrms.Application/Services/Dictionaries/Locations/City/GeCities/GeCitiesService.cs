using hrms.Domain.Models.Dictionary.Locations;
using hrms.Domain.Models.Vacations.Location;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Locations.City.GeCities
{
    public class GeCitiesService : IGeCitiesService
    {
        private readonly IRepository<Persistance.Entities.City> _citiesRepository;

        public GeCitiesService(IRepository<Persistance.Entities.City> citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public async Task<ServiceResult<List<CityDTO>>> Execute(CitiesFilter filter, CancellationToken cancellationToken)
        {
            var query = _citiesRepository.GetAllAsQueryable();

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
            if (filter.StateId.HasValue)
            {
                query = query.Where(x => x.StateId == filter.StateId.Value);
            }

            var result = await query.Select(x => new CityDTO()
            {
                Id = x.Id,
                CountryId = x.CountryId,
                StateId = x.StateId,
                SortIndex = x.SortIndex,
                Code = x.Code,
                Name = x.Name,
                IsActive = x.IsActive

            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<CityDTO>>.SuccessResult(result ?? new List<CityDTO>());
        }
    }
}
