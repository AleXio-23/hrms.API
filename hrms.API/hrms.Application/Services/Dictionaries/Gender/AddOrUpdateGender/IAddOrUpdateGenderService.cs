using hrms.Domain.Models.Dictionary.Gender;
using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.AddOrUpdateGender
{
    public interface IAddOrUpdateGenderService
    {
        Task<ServiceResult<GenderDTO>> Execute(GenderDTO gender, CancellationToken cancellationToken);
    }
}
