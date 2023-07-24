using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.DeleteDocumentType
{
    public interface IDeleteDocumentTypeService
    {
        Task<ServiceResult<bool>> Execute(int id, CancellationToken cancellationToken);
    }
}
