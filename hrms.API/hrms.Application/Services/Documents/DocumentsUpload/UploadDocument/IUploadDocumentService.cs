using hrms.Domain.Models.Documents;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace hrms.Application.Services.Documents.DocumentsUpload.UploadDocument
{
    public interface IUploadDocumentService
    {
        Task<ServiceResult<string>> Execute(UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken);
    }
}
