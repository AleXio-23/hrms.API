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
            if (!string.IsNullOrEmpty(filter.Value))
            {
                query = query.Where(x => x.Value.Contains(filter.Value));
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                query = query.Where(x => x.Description.Contains(filter.Description));
            }

            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }

            var result = await query.Select(x => new GenderDTO()
            {
                Id = x.Id,
                Value = x.Value,
                Description = x.Description,
                IsActive = x.IsActive
            }).ToListAsync(cancellationToken);

            return new ServiceResult<List<GenderDTO>>()
            {
                Success = true,
                ErrorOccured = false,
                Data = result ?? new List<GenderDTO>()
            };
        }
    }
}
