using AutoMapper;
using hrms.Domain.Models.Configuration;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration
{
    public class GetNumberTypesConfigurationService : IGetNumberTypesConfigurationService
    {
        private readonly IRepository<NumberTypesConfiguration> _numberTypeConfogirationRepository;
        private readonly IMapper _mapper;

        public GetNumberTypesConfigurationService(IRepository<NumberTypesConfiguration> numberTypeConfogirationRepository, IMapper mapper)
        {
            _numberTypeConfogirationRepository = numberTypeConfogirationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<NumberTypesConfigurationDTO>> Execute(int id, CancellationToken cancellationToken)
        {
            var getConfig = await _numberTypeConfogirationRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Configuration on Id {id} not found");

            var getMappedGConfig = _mapper.Map<NumberTypesConfigurationDTO>(getConfig);

            return ServiceResult<NumberTypesConfigurationDTO>.SuccessResult(getMappedGConfig);
        }

        public async Task<ServiceResult<NumberTypesConfigurationDTO>> Execute(string configName, CancellationToken cancellationToken)
        {
            var getConfig = await _numberTypeConfogirationRepository.FirstOrDefaultAsync(x => x.ConfigName == configName, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Configuration on Id {id} not found");

            var getMappedGConfig = _mapper.Map<NumberTypesConfigurationDTO>(getConfig);

            return ServiceResult<NumberTypesConfigurationDTO>.SuccessResult(getMappedGConfig);
        }
    }
}
