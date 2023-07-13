using hrms.Domain.Models.Dictionary.Gender;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.GetGenders
{
    public interface IGetGendersService
    {
        Task<ServiceResult<List<GenderDTO>>> Execute(GenderFilter filter, CancellationToken cancellationToken);
    }
}
