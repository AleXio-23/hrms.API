using hrms.Application.Services.Documents.DocumentsUpload.UploadDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument;
using hrms.Application.Services.Documents.DocumentTypes.AddOrUpdateDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.DeleteDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentTypes;

namespace hrms.Application.Services.Documents
{
    public class DocumentsFacade : IDocumentsFacade
    {
        public DocumentsFacade(IAddOrUpdateDocumentTypeService addOrUpdateDocumentTypeService, IDeleteDocumentTypeService deleteDocumentTypeService, IGetDocumentTypeService getDocumentTypeService, IGetDocumentTypesService getDocumentTypesService, IAddUploadedDocumentService addUploadedDocumentService, IAddUserUploadedDocumentService addUserUploadedDocumentService, IUploadDocumentService uploadDocumentService)
        {
            AddOrUpdateDocumentTypeService = addOrUpdateDocumentTypeService;
            DeleteDocumentTypeService = deleteDocumentTypeService;
            GetDocumentTypeService = getDocumentTypeService;
            GetDocumentTypesService = getDocumentTypesService;
            AddUploadedDocumentService = addUploadedDocumentService;
            AddUserUploadedDocumentService = addUserUploadedDocumentService;
            UploadDocumentService = uploadDocumentService;
        }

        public IAddOrUpdateDocumentTypeService AddOrUpdateDocumentTypeService { get; }

        public IDeleteDocumentTypeService DeleteDocumentTypeService { get; }

        public IGetDocumentTypeService GetDocumentTypeService { get; }

        public IGetDocumentTypesService GetDocumentTypesService { get; }

        public IAddUploadedDocumentService AddUploadedDocumentService { get; }

        public IAddUserUploadedDocumentService AddUserUploadedDocumentService { get; }

        public IUploadDocumentService UploadDocumentService { get; }
    }
}
