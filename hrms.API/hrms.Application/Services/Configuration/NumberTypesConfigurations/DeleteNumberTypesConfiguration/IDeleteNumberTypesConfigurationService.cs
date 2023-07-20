using hrms.Shared.Models;

namespace hrms.Application.Services.Configuration.NumberTypesConfigurations.DeleteNumberTypesConfiguration
{
    public interface IDeleteNumberTypesConfigurationService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
    }
}
