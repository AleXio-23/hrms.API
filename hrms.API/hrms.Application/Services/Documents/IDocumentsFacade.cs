using hrms.Application.Services.Documents.DocumentsUpload.UploadDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UploadedDocuments.AddUploadedDocument;
using hrms.Application.Services.Documents.DocumentsUpload.UserUploadedDocuments.AddUserUploadedDocument;
using hrms.Application.Services.Documents.DocumentTypes.AddOrUpdateDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.DeleteDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentTypes;

namespace hrms.Application.Services.Documents
{
    public interface IDocumentsFacade
    {
        IAddOrUpdateDocumentTypeService AddOrUpdateDocumentTypeService { get; }
        IDeleteDocumentTypeService DeleteDocumentTypeService { get; }
        IGetDocumentTypeService GetDocumentTypeService { get; }
        IGetDocumentTypesService GetDocumentTypesService { get; }

        IAddUploadedDocumentService AddUploadedDocumentService { get; }
        IAddUserUploadedDocumentService AddUserUploadedDocumentService { get; }
        IUploadDocumentService UploadDocumentService { get; }
    }
}
