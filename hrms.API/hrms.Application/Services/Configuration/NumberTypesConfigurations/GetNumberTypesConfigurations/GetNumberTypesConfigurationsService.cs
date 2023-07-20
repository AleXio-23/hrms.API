using AutoMapper;
using hrms.Domain.Models.Configuration;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations
{
    public class GetNumberTypesConfigurationsService : IGetNumberTypesConfigurationsService
    {
        private readonly IRepository<NumberTypesConfiguration> _numberTypesConfigurationRepository;
        private readonly IMapper _mapper;

        public GetNumberTypesConfigurationsService(IRepository<NumberTypesConfiguration> numberTypesConfigurationRepository, IMapper mapper)
        {
            _numberTypesConfigurationRepository = numberTypesConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<NumberTypesConfigurationDTO>>> Execute(NumberTypesConfigurationFilter filter, CancellationToken cancellationToken)
        {
            var query = _numberTypesConfigurationRepository.GetAllAsQueryable();

            if (!string.IsNullOrEmpty(filter.ConfigName))
            {
                query = query.Where(x => x.ConfigName.Contains(filter.ConfigName));
            }
            if (filter.IsActive != null)
            {
                query = query.Where(x => x.IsActive == filter.IsActive);
            }

            var result = await query.Select(x => _mapper.Map<NumberTypesConfigurationDTO>(x))
                                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return ServiceResult<List<NumberTypesConfigurationDTO>>.SuccessResult(result ?? new List<NumberTypesConfigurationDTO>());
        }
    }
}