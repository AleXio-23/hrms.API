using AutoMapper;
using hrms.Domain.Models.Configuration;
using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.AddorUpdateNumberTypesConfiguration
{
    public class AddOrUpdateNumberTypesConfigurationService : IAddOrUpdateNumberTypesConfigurationService
    {
        private readonly IRepository<NumberTypesConfiguration> _numberTypesConfigurationRepository;
        private readonly IMapper _mapper;

        public AddOrUpdateNumberTypesConfigurationService(IRepository<NumberTypesConfiguration> numberTypesConfigurationRepository, IMapper mapper)
        {
            _numberTypesConfigurationRepository = numberTypesConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<NumberTypesConfigurationDTO>> Execute(NumberTypesConfigurationDTO configurationDTO, CancellationToken cancellationToken)
        {
            if (configurationDTO?.Id == null)
            {
                var mappedNewConfig = _mapper.Map<NumberTypesConfiguration>(configurationDTO);
                var result = await _numberTypesConfigurationRepository.Add(mappedNewConfig, cancellationToken).ConfigureAwait(false);
                var resultDTO = _mapper.Map<NumberTypesConfigurationDTO>(result);
                return ServiceResult<NumberTypesConfigurationDTO>.SuccessResult(resultDTO);

            }
            if (configurationDTO?.Id != null && configurationDTO?.Id > 0)
            {
                var getExistingConfiguration = await _numberTypesConfigurationRepository
                    .Get(configurationDTO.Id, cancellationToken).ConfigureAwait(false)
                    ?? throw new NotFoundException($"Config on id {configurationDTO.Id} not found");

                getExistingConfiguration.Value = configurationDTO.Value;
                getExistingConfiguration.IsActive = configurationDTO.IsActive;

                var result = await _numberTypesConfigurationRepository.Update(getExistingConfiguration, cancellationToken).ConfigureAwait(false);
                var resultDto = _mapper.Map<NumberTypesConfigurationDTO>(result);
                return ServiceResult<NumberTypesConfigurationDTO>.SuccessResult(resultDto);
            }
            throw new ArgumentException($"Incorrect NumberTypeConfiguration Id type");
        }
    }
}
