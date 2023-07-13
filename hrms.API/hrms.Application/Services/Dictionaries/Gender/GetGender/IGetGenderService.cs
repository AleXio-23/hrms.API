using hrms.Domain.Models.Dictionary.Gender;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.GetGender
{
    public interface IGetGenderService
    {
        Task<ServiceResult<GenderDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
