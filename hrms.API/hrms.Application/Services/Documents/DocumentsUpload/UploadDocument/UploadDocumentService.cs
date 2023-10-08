using hrms.Application.Infranstructure.Interfaces.UserInterfaces;
using hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument;
using hrms.Domain.Models.Documents;
using hrms.Persistance.Entities;
using hrms.Shared.Models;

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

        public async Task<ServiceResult<UploadDocumentResponse>> Execute(UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken)
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

            var response = new UploadDocumentResponse()
            {
                UploadedDocumentDTO = new UploadedDocumentDTO()
                {
                    Id = addDocument.Data?.Id,
                    UploadDate = addDocument.Data?.UploadDate,
                    DocumentName = addDocument.Data?.DocumentName
                },
                UserUploadedDocumentDTO = new UserUploadedDocumentDTO()
                {
                    Id = addresult.Data?.Id,
                    UploadedByUserId = addresult.Data?.UploadedByUserId,
                    DocumentId = addresult.Data?.DocumentId,
                    UploadDate = addresult.Data?.UploadDate,
                    DocumentTypeId = addresult.Data?.DocumentTypeId,
                    DocumentTypeIfNotFoundInDicitonary = addresult.Data?.DocumentTypeIfNotFoundInDicitonary
                }
            };

            return ServiceResult<UploadDocumentResponse>.SuccessResult(response);
        }
    }
}
