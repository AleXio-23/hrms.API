using hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations;
using hrms.Application.Services.Configuration.NumberTypesConfigurations.UpdateNumberTypesConfiguration;

namespace hrms.Application.Services.Configuration
{
    public interface IConfigurationFacade
    {
        IUpdateNumberTypesConfigurationService UpdateNumberTypesConfiguration { get; }
        IDeleteNumberTypesConfigurationService DeleteNumberTypesConfigurationService { get; }
        IGetNumberTypesConfigurationService GetNumberTypesConfigurationService { get; }
        IGetNumberTypesConfigurationsService GetNumberTypesConfigurationsService { get; }
    }
}
