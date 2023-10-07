using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Locations.Country.DeleteCountry
{
    public class DeleteCountryService : IDeleteCountryService
    {
        private readonly IRepository<Persistance.Entities.Country> _countryRepository;

        public DeleteCountryService(IRepository<Persistance.Entities.Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            if (id < 1) throw new ArgumentException("Wrong value for id");
            var getGender = await _countryRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Gender on id: {id} not found");
            getGender.IsActive = false;
            await _countryRepository.Update(getGender, cancellationToken).ConfigureAwait(false);


            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
