using hrms.Domain.Models.Configuration;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.AddorUpdateNumberTypesConfiguration
{
    public interface IAddOrUpdateNumberTypesConfigurationService
    {
        Task<ServiceResult<NumberTypesConfigurationDTO>> Execute(NumberTypesConfigurationDTO configurationDTO, CancellationToken cancellationToken);
    }
}

