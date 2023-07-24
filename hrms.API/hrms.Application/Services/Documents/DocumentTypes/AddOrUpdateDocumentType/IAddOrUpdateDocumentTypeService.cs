using hrms.Domain.Models.Documents;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.AddOrUpdateDocumentType
{
    public interface IAddOrUpdateDocumentTypeService
    {
        Task<ServiceResult<DocumentTypeDTO>> Execute(DocumentTypeDTO documentTypeDTO, CancellationToken cancellationToken);
    }
}
