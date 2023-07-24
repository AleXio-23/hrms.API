using hrms.Domain.Models.Documents;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument
{
    public interface IAddUploadedDocumentService
    {
        Task<ServiceResult<UploadedDocumentDTO>> Execute(UploadedDocumentRequest uploadedDocumentRequest, CancellationToken cancellationToken);
    }
}
