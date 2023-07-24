using hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument;
using hrms.Domain.Models.Documents;
using hrms.Infranstructure.Services.CurrentUserId;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace hrms.Application.Services.Documents.DocumentsUpload.UploadDocument
{
    public class UploadDocumentService : IUploadDocumentService
    {
        private readonly IAddUserUploadedDocumentService _addUserUploadedDocumentService;
        private readonly IAddUploadedDocumentService _addUploadedDocumentService;
        private readonly IGetCurrentUserIdService _getCurrentUserIdService;

        public UploadDocumentService(IAddUserUploadedDocumentService addUserUploadedDocumentService, IAddUploadedDocumentService addUploadedDocumentService, IGetCurrentUserIdService getCurrentUserIdService)
        {
            _addUserUploadedDocumentService = addUserUploadedDocumentService;
            _addUploadedDocumentService = addUploadedDocumentService;
            _getCurrentUserIdService = getCurrentUserIdService;
        }

        public async Task<ServiceResult<string>> Execute(UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken)
        {
            var userId = _getCurrentUserIdService.Execute();
            var uloadDocObject = new UploadedDocumentRequest()
            {
                Document = uploadDocumentRequest.File
            };

            var addDocument = await _addUploadedDocumentService.Execute(uloadDocObject, cancellationToken).ConfigureAwait(false) ?? throw new ArgumentException("Document didn't uplaod");
            var userUploadObject = new UserUploadedDocumentDTO()
            {
                UploadedByUserId = userId,
                UploadDate = addDocument.Data?.UploadDate,
                DocumentTypeId = uploadDocumentRequest.DocumentTypeId,
                DocumentId = addDocument.Data?.Id,
                DocumentTypeIfNotFoundInDicitonary = uploadDocumentRequest.DocumentTypeIfNotFoundInDicitonary
            };

            var addresult = await _addUserUploadedDocumentService.Execute(userUploadObject, cancellationToken).ConfigureAwait(false);

            return ServiceResult<string>.SuccessResult("");
        }
    }
}
