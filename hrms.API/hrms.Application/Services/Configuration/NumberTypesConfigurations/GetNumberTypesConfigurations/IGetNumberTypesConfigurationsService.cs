using hrms.Domain.Models.Configuration;
using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.GetNumberTypesConfigurations
{
    public interface IGetNumberTypesConfigurationsService
    {
        Task<ServiceResult<List<NumberTypesConfigurationDTO>>> Execute(NumberTypesConfigurationFilter filter, CancellationToken cancellationToken);
    }
}
