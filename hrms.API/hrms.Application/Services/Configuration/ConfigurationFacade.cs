using hrms.Application.Services.Configuration.NumberTypesConfigurations.AddorUpdateNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations;

namespace hrms.Application.Services.Configuration
{
    public class ConfigurationFacade : IConfigurationFacade
    {
        public ConfigurationFacade(IAddOrUpdateNumberTypesConfigurationService addOrUpdateNumberTypesConfigurationService, IDeleteNumberTypesConfigurationService deleteNumberTypesConfigurationService, IGetNumberTypesConfigurationService getNumberTypesConfigurationService, IGetNumberTypesConfigurationsService getNumberTypesConfigurationsService)
        {
            AddOrUpdateNumberTypesConfigurationService = addOrUpdateNumberTypesConfigurationService;
            DeleteNumberTypesConfigurationService = deleteNumberTypesConfigurationService;
            GetNumberTypesConfigurationService = getNumberTypesConfigurationService;
            GetNumberTypesConfigurationsService = getNumberTypesConfigurationsService;
        }

        public IAddOrUpdateNumberTypesConfigurationService AddOrUpdateNumberTypesConfigurationService { get; }

        public IDeleteNumberTypesConfigurationService DeleteNumberTypesConfigurationService { get; }

        public IGetNumberTypesConfigurationService GetNumberTypesConfigurationService { get; }

        public IGetNumberTypesConfigurationsService GetNumberTypesConfigurationsService { get; }
    }
}
