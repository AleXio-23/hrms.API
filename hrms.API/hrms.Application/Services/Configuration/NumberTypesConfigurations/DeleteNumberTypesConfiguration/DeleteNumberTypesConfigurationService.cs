using hrms.Persistance.Entities;
using hrms.Persistance.Repository;
using hrms.Shared.Exceptions;
using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration
{
    public class DeleteNumberTypesConfigurationService : IDeleteNumberTypesConfigurationService
    {
        private readonly IRepository<NumberTypesConfiguration> _numberTypesConfigurationRepository;

        public DeleteNumberTypesConfigurationService(IRepository<NumberTypesConfiguration> numberTypesConfigurationRepository)
        {
            _numberTypesConfigurationRepository = numberTypesConfigurationRepository;
        }

        public async Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken)
        {
            var getConfig = await _numberTypesConfigurationRepository.Get(id, cancellationToken).ConfigureAwait(false) ?? throw new NotFoundException($"Configuration on Id {id} not found");
            getConfig.IsActive = false;
            await _numberTypesConfigurationRepository.Update(getConfig, cancellationToken).ConfigureAwait(false); ;

            return ServiceResult<bool>.SuccessResult(true);
        }
    }
}
