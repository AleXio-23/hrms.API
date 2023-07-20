using hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.UpdateNumberTypesConfiguration;

namespace hrms.Application.Services.Configuration
{
    public class ConfigurationFacade : IConfigurationFacade
    {
        public ConfigurationFacade(IAddOrUpdateNumberTypesConfigurationService updateNumberTypesConfiguration, IDeleteNumberTypesConfigurationService deleteNumberTypesConfigurationService, IGetNumberTypesConfigurationService getNumberTypesConfigurationService, IGetNumberTypesConfigurationsService getNumberTypesConfigurationsService)
        {
            UpdateNumberTypesConfiguration = updateNumberTypesConfiguration;
            DeleteNumberTypesConfigurationService = deleteNumberTypesConfigurationService;
            GetNumberTypesConfigurationService = getNumberTypesConfigurationService;
            GetNumberTypesConfigurationsService = getNumberTypesConfigurationsService;
        }

        public IAddOrUpdateNumberTypesConfigurationService UpdateNumberTypesConfiguration { get; }

        public IDeleteNumberTypesConfigurationService DeleteNumberTypesConfigurationService { get; }

        public IGetNumberTypesConfigurationService GetNumberTypesConfigurationService { get; }

        public IGetNumberTypesConfigurationsService GetNumberTypesConfigurationsService { get; }
    }
}
