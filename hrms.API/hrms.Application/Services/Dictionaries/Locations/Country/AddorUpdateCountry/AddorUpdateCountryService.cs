using AutoMapper;
using hrms.Domain.Models.Vacations.Location;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.AddorUpdateCountry
{
    public class AddorUpdateCountryService : IAddorUpdateCountryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Persistance.Entities.Country> _countryRepository;

        public AddorUpdateCountryService(IMapper mapper, IRepository<Persistance.Entities.Country> countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<ServiceResult<CountryDTO>> Execute(CountryDTO countryDTO, CancellationToken cancellationToken)
        {
            if (countryDTO.Id == null || countryDTO.Id < 1)
            {
                var newCountry = _mapper.Map<Persistance.Entities.Country>(countryDTO);
                var addResult = await _countryRepository.Add(newCountry, cancellationToken).ConfigureAwait(false);
                var returnResult = _mapper.Map<CountryDTO>(addResult);

                return ServiceResult<CountryDTO>.SuccessResult(returnResult);
            }
            else if (countryDTO.Id > 1)
            {
                var getCountry = await _countryRepository.Get(countryDTO.Id ?? -1, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Record on Id:{countryDTO.Id} not found");

                getCountry.Code = countryDTO.Code;
                getCountry.Name = countryDTO.Name;
                getCountry.HasStates = countryDTO.HasStates;
                getCountry.SortIndex = countryDTO.SortIndex;
                getCountry.IsActive = countryDTO.IsActive;

                var saveResult = await _countryRepository.Update(getCountry, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<CountryDTO>(saveResult);

                return ServiceResult<CountryDTO>.SuccessResult(resultDto);

            }
            else
            {
                throw new Exception("Unexpected error occured");
            }
        }
    }
}
