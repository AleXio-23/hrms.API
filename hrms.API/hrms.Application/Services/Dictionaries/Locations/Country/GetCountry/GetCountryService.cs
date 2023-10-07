using AutoMapper;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Vacations.Location;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.GetCountry
{
    public class GetCountryService : IGetCountryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Persistance.Entities.Country> _countryRepository;

        public GetCountryService(IMapper mapper, IRepository<Persistance.Entities.Country> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<ServiceResult<CountryDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                throw new ArgumentException("Wrong id value");
            }

            var getCountry = await _countryRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record with Id: {id} not found.");

            var countryDto = _mapper.Map<CountryDTO>(getCountry);

            return ServiceResult<CountryDTO>.SuccessResult(countryDto);
        }
    }
}
