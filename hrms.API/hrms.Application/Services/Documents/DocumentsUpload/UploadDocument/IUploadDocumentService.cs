using hrms.Domain.Models.Documents;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentsUpload.UploadDocument
{
    public interface IUploadDocumentService
    {
        Task<ServiceResult<UploadDocumentResponse>> Execute(UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken);
    }
}
