using hrms.Application.Services.Configuration.NumberTypesConfigurations.AddorUpdateNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations;

namespace hrms.Application.Services.Configuration
{
    public interface IConfigurationFacade
    {
        IAddOrUpdateNumberTypesConfigurationService AddOrUpdateNumberTypesConfigurationService { get; }
        IDeleteNumberTypesConfigurationService DeleteNumberTypesConfigurationService { get; }
        IGetNumberTypesConfigurationService GetNumberTypesConfigurationService { get; }
        IGetNumberTypesConfigurationsService GetNumberTypesConfigurationsService { get; }
    }
}
