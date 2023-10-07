using AutoMapper;
using hrms.Domain.Models.Dictionary.Locations;
using hrms.Domain.Models.Vacations.Location;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Locations.Country.GetCountries
{
    public class GetCountriesService : IGetCountriesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Persistance.Entities.Country> _countryRepository;

        public GetCountriesService(IMapper mapper, IRepository<Persistance.Entities.Country> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<ServiceResult<List<CountryDTO>>> Execute(CountryFilter filter, CancellationToken cancellationToken)
        {
            var query = _countryRepository.GetAllAsQueryable();

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

            if (filter.HasStates.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.HasStates.Value);
            }

            var result = await query.Select(x => new CountryDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                HasStates = x.HasStates,
                IsActive = x.IsActive,
                SortIndex = x.SortIndex
            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<CountryDTO>>.SuccessResult(result ?? new List<CountryDTO>());
        }
    }
}
