using hrms.Domain.Models.Documents;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.GetDocumentType
{
    public interface IGetDocumentTypeService
    {
        Task<ServiceResult<DocumentTypeDTO>> Execute(int id, CancellationToken cancellationToken);
    }
}
