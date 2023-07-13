using hrms.Shared.Models;

namespace hrms.Application.Services.Dictionaries.Gender.DeleteGender
{
    public interface IDeleteGenerService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
    }
}
