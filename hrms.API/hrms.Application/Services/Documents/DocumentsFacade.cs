using hrms.Application.Services.Documents.DocumentTypes.AddOrUpdateDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.DeleteDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentType;
using hrms.Application.Services.Documents.DocumentTypes.GetDocumentTypes;

namespace hrms.Application.Services.Documents
{
    public class DocumentsFacade : IDocumentsFacade
    {
        public DocumentsFacade(IAddOrUpdateDocumentTypeService addOrUpdateDocumentTypeService, IDeleteDocumentTypeService deleteDocumentTypeService, IGetDocumentTypeService getDocumentTypeService, IGetDocumentTypesService getDocumentTypesService)
        {
            AddOrUpdateDocumentTypeService = addOrUpdateDocumentTypeService;
            DeleteDocumentTypeService = deleteDocumentTypeService;
            GetDocumentTypeService = getDocumentTypeService;
            GetDocumentTypesService = getDocumentTypesService;
        }

        public IAddOrUpdateDocumentTypeService AddOrUpdateDocumentTypeService { get; }

        public IDeleteDocumentTypeService DeleteDocumentTypeService { get; }

        public IGetDocumentTypeService GetDocumentTypeService { get; }

        public IGetDocumentTypesService GetDocumentTypesService { get; }
    }
}
