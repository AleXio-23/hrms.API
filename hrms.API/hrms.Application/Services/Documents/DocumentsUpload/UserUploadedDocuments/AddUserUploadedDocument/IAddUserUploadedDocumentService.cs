using hrms.Domain.Models.Documents;
using hrms.Shared.Models;

namespace hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument
{
    public interface IAddUserUploadedDocumentService
    {
        Task<ServiceResult<UserUploadedDocumentDTO>> Execute(UserUploadedDocumentDTO userUploadedDocumentDTO, CancellationToken cancellationToken);
    }
}
