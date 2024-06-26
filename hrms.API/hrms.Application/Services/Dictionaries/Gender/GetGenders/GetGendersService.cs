using hrms.Domain.Models.Dictionary.Gender;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Dictionaries.Gender.GetGenders
{
    public class GetGendersService : IGetGendersService
    {
        private readonly IRepository<Persistance.Entities.Gender> _genderRepository;

        public GetGendersService(IRepository<Persistance.Entities.Gender> genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public async Task<ServiceResult<List<GenderDTO>>> Execute(GenderFilter filter, CancellationToken cancellationToken)
        {
            var query = _genderRepository.GetAllAsQueryable();
       
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }

            var result = await query.Select(x => new GenderDTO()
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<GenderDTO>>.SuccessResult(result ?? new List<GenderDTO>());
        }
    }
}
