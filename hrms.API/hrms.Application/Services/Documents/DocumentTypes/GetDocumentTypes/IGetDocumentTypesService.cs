using hrms.Domain.Models.Documents;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentTypes.GetDocumentTypes
{
    public interface IGetDocumentTypesService
    {
        Task<ServiceResult<List<DocumentTypeDTO>>> Execute(DocumentTypeFilter filter, CancellationToken cancellationToken);
    }
}
