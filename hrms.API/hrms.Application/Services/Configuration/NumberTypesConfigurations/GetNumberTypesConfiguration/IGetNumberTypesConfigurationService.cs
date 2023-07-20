using hrms.Domain.Models.Configuration;
using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfiguration
{
    public interface IGetNumberTypesConfigurationService
    {
        Task<ServiceResult<NumberTypesConfigurationDTO>> Execute(int id, CancellationToken cancellationToken);
        Task<ServiceResult<NumberTypesConfigurationDTO>> Execute(string configName, CancellationToken cancellationToken);
    }
}
