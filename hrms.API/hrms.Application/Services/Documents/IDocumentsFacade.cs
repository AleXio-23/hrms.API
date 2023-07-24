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
    }
}
